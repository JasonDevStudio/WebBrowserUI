function dataload() {
    $.get("http://api.app.local/bridge/dataload",{"Name":"ABC"},function(data){
        console.log(data)
    });
}