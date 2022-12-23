var user_logged_in = false;

const CLIENT_ID = 'ccd5e2b799eb4d0c9daf57324aa666ba';
const RESPONSE_TYPE = encodeURIComponent('token');
const REDIRECT_URI = encodeURIComponent('https://pkdiblkoedlhgfklhafpibplmogpdbpi.chromiumapp.org/');
const SCOPE = encodeURIComponent('user-read-email user-top-read');
const SHOW_DIALOG = encodeURIComponent('true');
let STATE = '';
let ACCESS_TOKEN = '';

function authorize(){
    STATE = encodeURIComponent('csh' + (Math.random() * (998999999) + 1000000).toString().substring(3,9));
    STATE += encodeURIComponent('ff' + Math.random().toString(36).substring(2, 16));
    let ouathurl = `https://accounts.spotify.com/authorize
?client_id=${CLIENT_ID}
&response_type=${RESPONSE_TYPE}
&redirect_uri=${REDIRECT_URI}
&scope=${SCOPE}
&state=${STATE}
&show_dialog=${SHOW_DIALOG}`;

    console.log(ouathurl);

    return ouathurl;
}


chrome.runtime.onMessage.addListener((request,sender,sendResponse) => {
    if (request.message === 'login'){
        if (user_logged_in){
            console.log("user already signed in!");
        }
        else{
            chrome.identity.launchWebAuthFlow({
                url: authorize(),
                interactive: true 
            }, function(redirect_url) {
                    console.log(redirect_url);
                    if (chrome.runtime.lastError) {
                        sendResponse({message: 'fail'});
                        console.log("There was a runtime error: ", chrome.runtime.lastError);
                    } 
                    else {
                       if (redirect_url.includes('callback?error=access_denied')){
                        sendResponse({message: 'fail'});
                        console.log("Access denied");
                       }
                       else{
                        let stateToken = redirect_url.substring(redirect_url.indexOf('state') + 6);
                        console.log("STATE: ", STATE);
                        console.log("State Token: ", stateToken);
                        if (stateToken === STATE){
                            user_logged_in = true;

                            setTimeout(() => {
                                ACCESS_TOKEN = '';
                                user_logged_in = false;
                            }, 3600000);
                            
                            ACCESS_TOKEN = redirect_url.substring(redirect_url.indexOf('access_token=') + 13);
                            ACCESS_TOKEN = ACCESS_TOKEN.substring(0,ACCESS_TOKEN.indexOf('&'));
                            console.log("access_token: ", ACCESS_TOKEN);
                            chrome.action.setPopup({ popup: 'popup_signed_out.html' }, () => {
                                sendResponse({ message: 'success' });
                            });
                        }
                        else{
                            sendResponse({message: 'fail'}); 
                        }  
                    } 
                    }
                });
            }
        return true;
    }
    else if(request.message === 'logout'){
        user_logged_in = false;
        chrome.action.setPopup({popup:'./popup.html'}, () => {
            sendResponse({message:'success'});
        });
        return true;
    }
});




