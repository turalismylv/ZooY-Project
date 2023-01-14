$(document).on('click', '#categor', function () {

    var id = $(this).data('id');
    console.log("salam")
    $.ajax({
        method: "GET",
        url: "/shop/categoryproduct",
        data: {
            id: id

        },
        success: function (result) {
            console.log(result)
            $('#Tural').empty().append("");
            $('#Tural').append(result);

        },
        error: function (e) {
            console.log(e)
        }
    })

})

$(document).on('click', '#brand', function () {

    var id = $(this).data('id');
    console.log("salam")
    $.ajax({
        method: "GET",
        url: "/shop/brandproduct",
        data: {
            id: id

        },
        success: function (result) {
            console.log(result)
            $('#Tural').empty().append("");
            $('#Tural').append(result);

        },
        error: function (e) {
            console.log(e)
        }
    })

})