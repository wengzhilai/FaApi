
var url = window.globalConfig.api;
$(function () {
    getQrCode()
})

//获取二维码
getQrCode = ()=>{
    $.ajax({
        type : "post",
        url : url+'/Staff/getStaff',
        data:JSON.stringify({Key:sessionStorage.getItem('id')}),
        datatype : 'json',
        contentType: "application/json; charset=utf-8",
        success:(res)=>{
            if (res.success == true){
                $('#qrCode').attr('src',res.data.qrCode)
            }
        },
        error:(err)=>{

        }
    })
};

// savePic=()=>{
//     var picurl= $("#qrCode").attr("src");
//     alert(picurl);
//     savePicture(picurl);
// }
//
// var triggerEvent = "touchstart";
//  savePicture=(Url)=>{
//     var blob=new Blob([''], {type:'application/octet-stream'});
//     var url = URL.createObjectURL(blob);
//     var a = document.createElement('a');
//     a.href = Url;
//     a.download = Url.replace(/(.*\/)*([^.]+.*)/ig,"$2").split("?")[0];
//     var e = document.createEvent('MouseEvents');
//     e.initMouseEvent('click', true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);
//     a.dispatchEvent(e);
//     URL.revokeObjectURL(url);
// };
