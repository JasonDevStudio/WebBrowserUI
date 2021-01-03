function load() {
    
    $.get("http://api.app.service/home/get",{"Name":"1234dfgdfg"}, function (data){
        console.log(data);
        var body = $("#mainBody");
        body.html(data.Name);
    }); 
}

function postData() {
    var name = $("#Name").val();
    var addr = $("#Addr").val();
    var json = {Name:name, Addr:addr};
    var data = JSON.stringify($("#mainForm").serializeArray());
    debugger

    $.ajax({url:"http://api.app.service/home/post", data:data, contentType:"application/json",processData:false, type:"POST", success:function(result){
            $("#mainBody").html(result);
        }});
    
   
}