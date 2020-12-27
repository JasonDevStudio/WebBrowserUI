function dataload() {
    $.get("http://api.app.service/bridge/dataload",{"Name":"ABC"},function(data){
        console.log(data)
    });
}

function imgload(){
    $("#imgaa").src = "http://local.app.service/images/20209121930471745.jpg";
}