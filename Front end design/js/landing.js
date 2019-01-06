const serverApi = "http://192.168.1.118:8000/";

$(() => {
    console.log(JSON.parse(window.sessionStorage.getItem('products')));

    var products = JSON.parse(window.sessionStorage.getItem('products'));
    products.forEach(pro => {
        console.log(pro.name)
        $("#containerGe").append(
            '<div class="container"><div id="container" class="row" style="background-color: white;border: 1px solid black;border-radius: 10px;">' +
            '<div id="image1" class="col-sm-3"><img src="' + pro.imageUrl + '" alt="test_tv" class="modulesTv"></div>' +
            '<div class="col-sm-7"><div class="col"><h4 class="items"> <span id="moduleTitle"></span>' + pro.name + '</h4>' +
            '<p class="description"><span id="moduleText"></span>' + pro.description + '</p></div><p class="price">' + pro.price + '</p>' +
            "</div><div class=\"col-sm-2\"><img src=\"./src/ebay_icon.png\" onclick=\"selectionFromUser('" + pro.link + "', '" + pro.id + "', '" + pro.description + "')\" alt=\"ebay_link\"/></div></div><br></div>");
    })
});

function selectionFromUser(link, product, description) {
    window.open(link, '_blank');

    var email = getCookie('id');
    if (email) {
        $.ajax({
            url: serverApi + "selected?" +
                "id=" + email +
                "&ebayId=" + product +
                "&link=" + link +
                "&description=" + description,
            success: function () {
                console.log("Saved.")
            }
        });
    }
}

function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}