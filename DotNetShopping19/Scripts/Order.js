function statusButtonClick(sender, orderId) {
    //alert(sender.value);
    var shippingCode = '';
    if (sender.value === '3') {
        shippingCode = $('#ShippingCode').val();
        //alert(shippingCode);
    }
    var dataToPost = {
        orderId: orderId,
        status: sender.value,
        shippingCode: shippingCode
    };
    $.post('/Order/UpdateStatus', dataToPost)
        .done(function (response, status, jqxhr) {
            if (response['Success'] === true) {
                //alert('Success');
                location.reload();
            }
            else {
                alert(response['Error']);
            }
        })
        .fail(function (jqxhr, status, error) {
            // this is the ""error"" callback
            alert('Error!');
        });
}
function statusUpdateClick(orderId) {

    var dataToPost = {
        orderId: orderId,
        status: $('#OrderStatus').val(),
        shippingCode: ''
    };
    $.post('/Order/UpdateStatus', dataToPost)
        .done(function (response, status, jqxhr) {
            if (response['Success'] === true) {
                //alert('Success');
                location.reload();
            }
            else {
                alert(response['Error']);
            }
        })
        .fail(function (jqxhr, status, error) {
            // this is the ""error"" callback
            alert('Error!');
        });
}