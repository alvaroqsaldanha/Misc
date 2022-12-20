#include <stdio.h>
#include <stdlib.h>
#include <time.h>

typedef struct edge{
	int flux;
	int index;
} edge;

typedef struct intersection{
	int flux;
	int height;
    int present;		/*2:Market(s) 1:House(s) 0:Empty 3:House and Market */
    edge * edges;
    int fluxer;
} Intersection;

typedef struct superSource{
	int height;
	int flux;
	edge * edges;
} superSource;

typedef struct superSink{
	int height;
	int flux;
} superSink;

typedef struct fluxer{
	int index;
	struct fluxer * next;
} fluxer;

Intersection *city; 	/*Indexes of intersections are calculated by the formula (street-1)*nAvn + av */
int nAvn , nStreets, nMarkets, nHouses;
int nIntersections;
int totalOutsiders=0;
superSource source;
superSink sink;
fluxer * head = NULL;


int right(int index){
    if((((index) % nAvn)==0)) return 0;
    return index+1;
}

int left(int index){
    if((right(index-1) == 0)||index-1==0) return 0;
    return index-1;
}

int up(int index){
    if(index<=nAvn) return 0;
    return index-nAvn;
}

int down(int index){
    if(index>nAvn*(nStreets-1)) return 0;
    return index+nAvn;
}

void initializeMarkets(){
	int markets = nMarkets;
	int av=0, street=0 ,i=0;
	for(;markets>0;markets--){
        scanf("%d %d", &av, &street);
        i=(street-1)*nAvn+av;
        if(city[i].present==2) nMarkets--;
        else{
            city[i+nIntersections].edges=malloc(sizeof(edge)*2);
            city[i].edges=malloc(sizeof(edge)*5);
            city[i].edges[4].flux = 0;
            city[i].edges[4].index = i + nIntersections;
            city[i + nIntersections].edges[1].flux = 0;
            city[i + nIntersections].edges[1].index = i;
            city[i].present=2;
            city[i+nIntersections].present = 2;
            city[i].flux=0;
            city[i+nIntersections].flux = 0;
            city[i+nIntersections].edges[0].flux = 0;
            city[i+nIntersections].edges[0].index = -2;           
        } 
}
}

void initializeHouses(){
	int houses = nHouses;
	int av=0, street=0 ,i=0;
	fluxer * temp, * prev = NULL, *j;
	for(;houses>0;houses--){
        scanf("%d %d", &av, &street);
        i=(street-1)*nAvn+av;
        if(city[i].present==1) nHouses--;
        else if(city[i].present==2){
            city[i].present=1;
            city[i+nIntersections].present=1;
            city[i].flux = 0;
            city[i+nIntersections].flux = 0;
            nHouses--;
            nMarkets--;
            totalOutsiders++;


        }
        else if(city[i].present==3){
            nHouses--;
        }
        else{      	
			city[i].edges=malloc(sizeof(edge) * 2);
            city[i+nIntersections].edges=malloc(sizeof(edge)*5);
            city[i].edges[1].flux = 0;
            city[i].edges[1].index = i + nIntersections;
            city[i + nIntersections].edges[4].flux = 0;
            city[i + nIntersections].edges[4].index = i;
            city[i].present=1;
            city[i + nIntersections].present=1;
            city[i].flux = 1;
            city[i + nIntersections].flux = 0;
            city[i].edges[0].index = -1;
            city[i].edges[0].flux = -1;
            temp = malloc(sizeof(fluxer));
            temp->index = i;
            if ((head == NULL) || (head->index > temp->index)){
            	temp->next = head;
            	head = temp;
            }
            else{
            	j = head;
            	while ((j != NULL) && (temp->index > j->index)){
            		prev = j;
            		j = j->next;
            	}
            	temp->next = j;
            	prev->next = temp;
            }
        }
    }
}

void initializeSuper(){
	source.height = 2 * nIntersections + 2;
	source.flux = -nHouses;
	sink.height = 0;
	sink.flux = 0;
}

void initializecity(){
    int i,edgeIndex = 0;
    city=malloc(sizeof(Intersection)*(2*nIntersections+1));
    initializeMarkets();
    initializeHouses();
    initializeSuper();
    for(i=1;i<=nIntersections;i++){
        if(city[i].present==1){
            city[i + nIntersections].edges[edgeIndex].index=up(i);
            city[i + nIntersections].edges[edgeIndex++].flux=0;
            city[i + nIntersections].edges[edgeIndex].index=left(i);
            city[i + nIntersections].edges[edgeIndex++].flux=0;
            city[i + nIntersections].edges[edgeIndex].index=right(i);
            city[i + nIntersections].edges[edgeIndex++].flux=0;
            city[i + nIntersections].edges[edgeIndex].index=down(i);
            city[i + nIntersections].edges[edgeIndex++].flux=0;
        }
        else if(city[i].present==2){
            if (up(i) != 0) city[i].edges[edgeIndex].index=up(i) +nIntersections;
            else city[i].edges[edgeIndex].index = 0;
            city[i].edges[edgeIndex++].flux=0;
            if (left(i) != 0) city[i].edges[edgeIndex].index=left(i) +nIntersections;
            else city[i].edges[edgeIndex].index = 0;
            city[i].edges[edgeIndex++].flux=0;
            if (right(i) != 0) city[i].edges[edgeIndex].index=right(i) +nIntersections;
            else city[i].edges[edgeIndex].index = 0;
            city[i].edges[edgeIndex++].flux=0;
            if (down(i) != 0) city[i].edges[edgeIndex].index=down(i) +nIntersections;
            else city[i].edges[edgeIndex].index = 0;
            city[i].edges[edgeIndex++].flux=0;
        }
        else{
            city[i].edges=malloc(sizeof(edge)*5);
            city[i+nIntersections].edges=malloc(sizeof(edge)*5);
            city[i].edges[4].flux = 0;
            city[i].edges[4].index = i + nIntersections;
            city[i + nIntersections].edges[4].flux = 0;
            city[i + nIntersections].edges[4].index = i;
            if (up(i) != 0) city[i].edges[edgeIndex].index=up(i) +nIntersections;
            else city[i].edges[edgeIndex].index = 0;
            city[i].edges[edgeIndex].flux=0;
            city[i+nIntersections].edges[edgeIndex].index=up(i);
            city[i+nIntersections].edges[edgeIndex++].flux=0;
            if (left(i) != 0) city[i].edges[edgeIndex].index=left(i) +nIntersections;
            else city[i].edges[edgeIndex].index = 0;
            city[i].edges[edgeIndex].flux=0;
            city[i+nIntersections].edges[edgeIndex].index=left(i);
            city[i+nIntersections].edges[edgeIndex++].flux=0;
            if (right(i) != 0) city[i].edges[edgeIndex].index=right(i) +nIntersections;
            else city[i].edges[edgeIndex].index = 0;
            city[i].edges[edgeIndex].flux=0;
            city[i+nIntersections].edges[edgeIndex].index=right(i);
            city[i+nIntersections].edges[edgeIndex++].flux=0;
            if (down(i) != 0) city[i].edges[edgeIndex].index=down(i) +nIntersections;
            else city[i].edges[edgeIndex].index = 0;
            city[i].edges[edgeIndex].flux=0;
            city[i+nIntersections].edges[edgeIndex].index=down(i);
            city[i+nIntersections].edges[edgeIndex++].flux=0;
            city[i].present = 0;
            city[i+nIntersections].present = 0;
        }
        edgeIndex = 0; 
        city[i].height = 0;
        city[i+nIntersections].height = 0;
    }

}

void addToList(int indexV){
	fluxer * temp, * prev = NULL, * j;
	if (city[indexV].flux == 0){
		temp = malloc(sizeof(fluxer));
	    temp->index = indexV;
		temp->next = NULL;
		if ((head == NULL) || (head->index > temp->index)){
        	temp->next = head;
        	head = temp;
    	}
    	else{
        	j = head;
        	while ((j != NULL) && (temp->index > j->index)){
        		prev = j;
        		j = j->next;
        	}
        	temp->next = j;
        	prev->next = temp;
    	}
    }

}




void push(int indexU,int indexV,int edgeIndex){
	int i,keep;
	city[indexU].flux--;
	city[indexV].flux++;
	keep =city[indexU].edges[edgeIndex].flux;
	city[indexU].edges[edgeIndex].flux++;
	for (i=0;i<5;i++){
		if(city[indexV].edges[i].index == indexU){
			city[indexV].edges[i].flux--;
			break;
		}
	}
	if ((city[indexV].present != 2) && (city[indexU].present != 1) && (keep == 0) && (edgeIndex != 4)){
		for (i=0;i<4;i++){
		if(city[indexV + nIntersections].edges[i].index == indexU - nIntersections){
			city[indexV + nIntersections].edges[i].flux--;
			break;
		}
	}
	for (i=0;i<4;i++){
		if(city[indexU- nIntersections].edges[i].index == indexV + nIntersections){
			city[indexU - nIntersections].edges[i].flux++;
			break;
		}
	}
	}
	else if ((city[indexV].present != 1) && (city[indexU].present != 2) && (keep == -1) && (edgeIndex != 4)){
		for (i=0;i<4;i++){
		if(city[indexV - nIntersections].edges[i].index == indexU + nIntersections){
			city[indexV - nIntersections].edges[i].flux--;
			break;
		}
	}
	for (i=0;i<4;i++){
		if(city[indexU + nIntersections].edges[i].index == indexV - nIntersections){
			city[indexU + nIntersections].edges[i].flux++;
			break;
		}
	}
	}


}

void discharge(int indexU){
	int i,indexV = -1,edgeIndex = -1, minHeight = 4*nIntersections -1;
	fluxer * temp = NULL;
	if (head != NULL){
		temp = head;
		head = head->next;
		free(temp);
	}
	while (city[indexU].flux != 0){
		if (indexU <= nIntersections){
			if (city[indexU].present == 1){
				for (i = 1; i > -1; i--){
					if((city[indexU].edges[i].index != 0) && (((i == 1) && (city[indexU].height == city[city[indexU].edges[i].index].height + 1)) || ((i == 0) && (city[indexU].height == source.height + 1))) && ((city[indexU].edges[i].flux == 0)  || (city[indexU].edges[i].flux == -1))){
						indexV = city[indexU].edges[i].index;
						edgeIndex = i;
						break;
					}
					if((city[indexU].edges[i].index != 0) && ((city[indexU].edges[i].flux == 0) || (city[indexU].edges[i].flux == -1)) && (((i == 1) && (city[city[indexU].edges[i].index].height < minHeight)) || ((i == 0) && (source.height < minHeight)))){
						minHeight = city[city[indexU].edges[i].index].height;
					}
				}
				if (edgeIndex == 1){;
					addToList(indexU + nIntersections);
					city[indexU].flux--;
					city[indexU + nIntersections].flux++;
					city[indexU].edges[1].flux = 1;
					city[indexU + nIntersections].edges[4].flux = -1;
				}
				else if(edgeIndex == 0){
					source.flux++;
					city[indexU].flux--;
				}
				else{
					city[indexU].height = minHeight + 1;
				} 
				
			}
			else{
				for (i = 4;i > -1;i--){
					if((city[indexU].edges[i].index != 0) && (city[indexU].height == city[city[indexU].edges[i].index].height + 1) && (((city[indexU].edges[i].flux == 0) && (i == 4)) || (city[indexU].edges[i].flux == -1))){
						indexV = city[indexU].edges[i].index;
						edgeIndex = i;
						break;
					}
					if((city[indexU].edges[i].index != 0) && (((city[indexU].edges[i].flux == 0)&& (i == 4)) || (city[indexU].edges[i].flux == -1)) && (city[city[indexU].edges[i].index].height < minHeight) && (city[city[indexU].edges[i].index].height > city[indexU].height-1 )){
						minHeight = city[city[indexU].edges[i].index].height;
					}
				}
				if ((indexV != -1) && (edgeIndex != -1)){
					addToList(indexV);
					push(indexU,indexV,edgeIndex);
				}
				else{
					city[indexU].height = minHeight + 1;
				}
				
				}



			}
		else{
			if(city[indexU].present == 2){
				sink.flux++;
				city[indexU].height = sink.height + 1;
				city[indexU].flux--;
				totalOutsiders++;
			}
			else{
			for (i = 0;i < 5;i++){
				if((city[indexU].edges[i].index != 0) && (city[indexU].height == city[city[indexU].edges[i].index].height + 1) && (((city[city[indexU].edges[i].index].present != 1)   && (city[indexU].edges[i].flux == 0)) || ((city[indexU].edges[i].flux == -1) && (i == 4)))){
					indexV = city[indexU].edges[i].index;
					edgeIndex = i;
					break;
				}
				if((city[indexU].edges[i].index != 0) && (((city[city[indexU].edges[i].index].present != 1)  &&(city[indexU].edges[i].flux == 0)) || ((city[indexU].edges[i].flux == -1) && (i == 4))) && (city[city[indexU].edges[i].index].height < minHeight) && (city[city[indexU].edges[i].index].height > city[indexU].height-1 )){
					minHeight = city[city[indexU].edges[i].index].height;
				}
			}
			if ((indexV != -1) && (edgeIndex != -1)){
				addToList(indexV);
				push(indexU,indexV,edgeIndex);
			}
			else{
				city[indexU].height = minHeight + 1;
			}
			
		}
	}
		
		indexV = -1;
		edgeIndex = -1;
		minHeight = 4*nIntersections -1;
		
	}

}





int algorithm(){
	while ((sink.flux != -(source.flux) && (sink.flux != nMarkets))){
		discharge(head->index);
	}
    return totalOutsiders;
} 

int main(){
	clock_t t1,t0;
	t1 = clock();
    scanf("%d %d", &nAvn, &nStreets);
    scanf("%d %d", &nMarkets, &nHouses);
    nIntersections = nAvn * nStreets;
    if ((nMarkets == 0) || (nHouses == 0) || (nIntersections == 0)){
    	printf("0\n");
    	t0 = clock() - t1;
    	double time = ((double)t0)/CLOCKS_PER_SEC;
    	printf("%f\n",time); 
    	return 0;
    } 
    initializecity();
    printf("%d\n",algorithm());
    t0 = clock() - t1;
	double time = ((double)t0)/CLOCKS_PER_SEC;
	printf("%f\n",time); 
    return 0;
}