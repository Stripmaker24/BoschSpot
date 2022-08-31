var cateSelect = document.getElementById("categorySelect");
var contentSelect = document.getElementById("contenderSelect");

function removeOptions(selectElement) {
    var i, L = selectElement.options.length - 1;
    for (i = L; i >= 0; i--) {
        selectElement.remove(i);
    }
}

$.ajax({
    type: 'GET',
    url: '/GetCategories',
    success: function (result) {
        for (var i = 0; i < result.length; i++) {
            var opt = document.createElement("option");
            opt.value = result[i].id;
            opt.innerHTML = result[i].category;

            cateSelect.appendChild(opt);
        }
    }
});
$.ajax({
    type: 'GET',
    url: '/GetContendersOfCategory',
    data: { ID: cateSelect.value },
    success: function (result) {
        for (var i = 0; i < result.length; i++) {
            var opt = document.createElement("option");
            opt.value = result[i].id;
            opt.innerHTML = result[i].name;

            contentSelect.appendChild(opt);
        }
    }
})
cateSelect.onchange = function () {
    $.ajax({
        type: 'GET',
        url: '/GetContendersOfCategory',
        data: { ID: cateSelect.value },
        success: function (result) {
            removeOptions(contentSelect);
            for (var i = 0; i < result.length; i++) {
                var opt = document.createElement("option");
                opt.value = result[i].id;
                opt.innerHTML = result[i].name;

                contentSelect.appendChild(opt);
            }
        }
    })
};