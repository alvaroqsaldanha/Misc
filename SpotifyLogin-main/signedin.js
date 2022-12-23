document.querySelector('.signoutbutton').addEventListener('click',function(){
    chrome.runtime.sendMessage({message: 'logout'}, function(response) {
        if (response.message === 'success') window.close();
    });
});