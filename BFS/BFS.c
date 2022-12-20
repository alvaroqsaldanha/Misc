#include <stdio.h>
#include <stdlib.h>

#define WHITE 0
#define BLACK 2

typedef struct node{
	int grade;
	int color;
} Node;

typedef struct adjListNode{
	int index;
	struct adjListNode * next;
} adjListNode;

typedef struct gradeListNode{
	int index;
	struct gradeListNode * next;
} gradeListNode;

Node * graph;
adjListNode ** adjList;
gradeListNode * gradeList[21] = {NULL};
int nCount, eCount, _nCount, high = 0;
gradeListNode * rightNode;


void initialize_nodes(){
	int i, tempGrade;
	for (i = 0; i < nCount; i++){
		scanf("%d",&tempGrade);
		graph[i].grade = tempGrade;
		graph[i].color = WHITE;
		adjList[i] = NULL;
		gradeListNode * temp = malloc(sizeof(gradeListNode));
		temp->index = i;
		if (gradeList[tempGrade] == NULL){
			temp->next = NULL;
		}
		else{
			temp->next = gradeList[tempGrade];
		}
		gradeList[tempGrade] = temp;
		if (tempGrade > high) high = tempGrade;
	}

}

void connect(int node1, int node2){
	adjListNode * temp = malloc(sizeof(adjListNode));
	temp->index = node2;
	if (adjList[node1] == NULL){
		temp->next = NULL;
		adjList[node1] = temp;
	}
	else{
		temp->next = adjList[node1];
		adjList[node1] = temp;
	}
}

void initialize_connections(){
	int i,node1, node2;
	for (i = 0; i < eCount; i++){
		scanf("%d %d",&node2,&node1);
		connect(node1-1,node2-1);
	}
}

int findHighestGrade(){
	int i;
	gradeListNode * temp;
	for (i = high; i > -1; i--){
		for(temp = rightNode; temp != NULL;temp = temp->next){
			if(graph[temp->index].color == WHITE){
				high = i;
				return temp->index;
			}
		}
		rightNode = gradeList[i - 1];
	}
	return 0;
}

void propagate(int node, int grade){
	adjListNode * temp = adjList[node];
	graph[node].color = BLACK;
	graph[node].grade = grade;
	_nCount--;
	if (_nCount == 0) return;
	for(;temp != NULL;temp = temp->next){
		if(graph[temp->index].color == WHITE){
			propagate(temp->index,grade);
		}
	}
}

void algorithm(){
	int bigNode;
	while(_nCount>0){
		bigNode=findHighestGrade();
		propagate(bigNode,graph[bigNode].grade);	
	}
}

void output(){
	int i=0;
	for (;i<nCount;i++){
		printf("%d\n",graph[i].grade);
	}
}

int main(){
	scanf("%d, %d",&nCount,&eCount);
	graph = malloc(sizeof(Node)*nCount);
	adjList = malloc(sizeof(adjListNode*) * nCount);
	_nCount = nCount;
	initialize_nodes();
	initialize_connections();
	rightNode = gradeList[high];
	algorithm();
	output();
	free(adjList);
	free(graph);
	return 0;
}