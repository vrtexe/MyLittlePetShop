var pageNum = 1;
$('.js-load-next').click(function () {
    pageNum++;
    $.ajax({
        method: "GET",
        url: "/Home/GetData",
        data: { "page": pageNum },
        dataType: 'json',
        success: function (data) {
            let col = $(".js-load-prev").parent().parent();
            let currRow = $(".table td").first();
            for (var i = 0; i < 5; i++) {
                currRow.next().remove();
            }
            for (var i = 0; i < data.length; i++) {
                let n = data[i].id;
                currRow.after('<td class="col-md-2 p-3 mr-0 border-0 btn-link"' + 'onclick=location.href=Items/Details/?id=' + data[i].id + ' style="height:1px" colspan="3" rowspan="6">' +
                    '<div class="row">' +
                    '<div class="col-md-12 p-0">' +
                    '<input type="image" src="' + data[i].Image + '" style="height:inherit;" class="form-control"  />' +
                    '</div>' +
                    '</div>' +
                    '<div class="row">' +
                    '<div class="col-md-7 p-0 text-center form-control">' +
                    data[i].Name +
                    '</div>' +
                    '<div class="col-md-5 p-0 text-center form-control">' +
                    data[i].Price +
                    '</div>' +
                    '</div>' +
                    '</td>')
                currRow = currRow.next()
            }
            for (var i = data.length; i < 5; i++) {
                currRow.after('<td class="col-md-2"></td>')
                currRow = currRow.next()
            }
        }
    })
})

$('.js-load-prev').click(function () {
    if (pageNum - 1 >= 1) {
        pageNum--;
    }
    $.ajax({
        method: "GET",
        url: "/Home/GetData",
        data: { "page": pageNum },
        dataType: 'json',
        success: function (data) {
            row = $(".js-load-prev").parent().parent();
            let currRow = $(".table td").first();
            for (var i = 0; i < 5; i++) {
                currRow.next().remove();
            }
            for (var i = 0; i < data.length; i++) {
                currRow.after('<td class="col-md-2 p-3 mr-0 border-0 btn-link"' + 'onclick=location.href=Items/Details/?id=' + data[i].id + ' style="height:1px" colspan="3" rowspan="6">' +
                    '<div class="row">' +
                    '<div class="col-md-12 p-0">' +
                    '<input type="image" src="' + data[i].Image + '" style="height:inherit;" class="form-control"  />' +
                    '</div>' +
                    '</div>' +
                    '<div class="row">' +
                    '<div class="col-md-7 p-0 text-center form-control">' +
                    data[i].Name +
                    '</div>' +
                    '<div class="col-md-5 p-0 text-center form-control">' +
                    data[i].Price +
                    '</div>' +
                    '</div>' +
                    '</td>')
                currRow = currRow.next()
            }
            for (var i = data.length; i < 5; i++) {
                currRow.after('<td class="col-md-2"></td>')
                currRow = currRow.next()
            }
        }
    })
})

var pageNum1 = 1;
$('.js-load-next-new').click(function () {
    pageNum1++;
    $.ajax({
        method: "GET",
        url: "/Home/GetDataNew",
        data: { "page": pageNum1 },
        dataType: 'json',
        success: function (data) {
            let col = $(".js-load-prev").parent().parent();
            let currRow = $(".table1 td").first();
            for (var i = 0; i < 5; i++) {
                currRow.next().remove();
            }
            for (var i = 0; i < data.length; i++) {
                let n = data[i].id;
                currRow.after('<td class="col-md-2 p-3 mr-0 border-0 btn-link"' + 'onclick=location.href=Items/Details/?id=' + data[i].id + ' style="height:1px" colspan="3" rowspan="6">' +
                    '<div class="row">' +
                    '<div class="col-md-12 p-0">' +
                    '<input type="image" src="' + data[i].Image + '" style="height:inherit;" class="form-control"  />' +
                    '</div>' +
                    '</div>' +
                    '<div class="row">' +
                    '<div class="col-md-7 p-0 text-center form-control">' +
                    data[i].Name +
                    '</div>' +
                    '<div class="col-md-5 p-0 text-center form-control">' +
                    data[i].Price +
                    '</div>' +
                    '</div>' +
                    '</td>')
                currRow = currRow.next()
            }
            for (var i = data.length; i < 5; i++) {
                currRow.after('<td class="col-md-2"></td>')
                currRow = currRow.next()
            }
        }
    })
})

$('.js-load-prev-new').click(function () {
    if (pageNum1 - 1 >= 1) {
        pageNum1--;
    }
    $.ajax({
        method: "GET",
        url: "/Home/GetDataNew",
        data: { "page": pageNum1 },
        dataType: 'json',
        success: function (data) {
            row = $(".js-load-prev").parent().parent();
            let currRow = $(".table1 td").first();
            for (var i = 0; i < 5; i++) {
                currRow.next().remove();
            }
            for (var i = 0; i < data.length; i++) {
                currRow.after('<td class="col-md-2 p-3 mr-0 border-0 btn-link"' + 'onclick=location.href=Items/Details/?id=' + data[i].id + ' style="height:1px" colspan="3" rowspan="6">' +
                    '<div class="row">' +
                    '<div class="col-md-12 p-0">' +
                    '<input type="image" src="' + data[i].Image + '" style="height:inherit;" class="form-control"  />' +
                    '</div>' +
                    '</div>' +
                    '<div class="row">' +
                    '<div class="col-md-7 p-0 text-center form-control">' +
                    data[i].Name +
                    '</div>' +
                    '<div class="col-md-5 p-0 text-center form-control">' +
                    data[i].Price +
                    '</div>' +
                    '</div>' +
                    '</td>')
                currRow = currRow.next()
            }
            for (var i = data.length; i < 5; i++) {
                currRow.after('<td class="col-md-2"></td>')
                currRow = currRow.next()
            }
        }
    })
})