$(".js-delete").click(function () {
    let btn = $(this);
    bootbox.confirm("Are you sure you would liek to delete category ?", function (confirm) {
        if (confirm) {
            $.ajax({
                method: "DELETE",
                url: "api/CategoriesAPI/" + btn.attr("thisId"),
                success: function () {
                    $(".table").row(btn.parent().parent()).remove().draw()
                }
            })
        }
    })
})