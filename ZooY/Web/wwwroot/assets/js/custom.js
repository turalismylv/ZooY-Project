$(document).ready(function () {


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

    $(document).on('click', '#addToCart', function () {
        var id = $(this).data('id');

        $.ajax({
            method: "POST",
            url: "/basket/AddProduct",
            data: {
                id: id

            },
            success: function (result) {

                console.log("Avara")
            }

        })
    })




    $(document).on('click', '#deleteButton', function () {
        var id = $(this).data('id');

        var minibasketcount = $('#minibasketcount').html();
        var quantity = $(this).data('quantity');

        var sum = minibasketcount - quantity;


        console.log("sil")

        $.ajax({
            method: "POST",
            url: "/basket/delete",
            data: {
                id: id
            },
            success: function (result) {
                $(`.basketProduct[id=${id}]`).remove();

                $('#minibasketcount').html("");
                $('#minibasketcount').append(sum);
            }

        })
    })

    $(document).on('click', '#upcount', function () {
        var id = $(this).data('id');
        console.log("artir")
        $.ajax({
            method: "POST",
            url: "/basket/UpCountProduct",
            data: {
                id: id
            },
            success: function (result) {
                console.log(result);
            }

        })
    })

    $(document).on('click', '#downcount', function () {
        var id = $(this).data('id');

        $.ajax({
            method: "POST",
            url: "/basket/downcountproduct",
            data: {
                id: id
            },
            success: function (result) {
                console.log(result);
            }

        })
    })
})



