$(() => {
    var products = JSON.parse(window.sessionStorage.getItem('products'));
    products.forEach(pro => {
        var productDiv = document.createElement('div');
        productDiv.className = "row";
        productDiv.style = "background-color: white;box-shadow: -1px 1px 35px -10px rgba(0,0,0,0.75);border-radius: 10px;padding:10px";

        var productImageDiv = document.createElement('div');
        productImageDiv.className = "col-3 middleImage";
        productImageDiv.style = "display: flex;align-items: center;flex-wrap: wrap;";

        var image = document.createElement('img');
        image.src = pro.imageUrl;
        image.style = "width:100%;"
        productImageDiv.append(image);

        var productDescDiv = document.createElement('div');
        productDescDiv.className = "col-7";

        var titleBar = document.createElement('h3');
        titleBar.innerHTML = pro.name;
        titleBar.style = "font-family: Montserrat;font-size:125%; margin-top:10px;";

        var desc = document.createElement('p');
        var g = "<br/><ul>";
        pro.description.split(",").forEach(d => {
            g += '<li style="font-family: Montserrat;font-size:100%;">' + d.split(":")[0].capitalize() + " : " + d.split(":")[1].capitalize() + "</li>";
        });
        desc.innerHTML = g + "</ul>";

        var price = document.createElement('p');
        price.innerHTML = pro.price;
        price.style = "display:block;text-align:right;font-family: Montserrat;font-size:125%;color:rgb(109, 186, 58);";

        productDescDiv.append(titleBar);
        productDescDiv.append(desc);
        productDescDiv.append(price);

        var productLinkDiv = document.createElement('div');
        productLinkDiv.className = "col-2 middleImage";
        productLinkDiv.style = "display: flex;align-items: center;flex-wrap: wrap;";

        var image2 = document.createElement('img');
        image2.src = "./src/ebay_logo.png";
        image2.onclick = () => { selectionFromUser(pro.link, pro.id, pro.description) };
        image2.style = "width:100%;cursor:pointer";
        productLinkDiv.append(image2);

        var salto = document.createElement('br');

        productDiv.append(productImageDiv);
        productDiv.append(productDescDiv);
        productDiv.append(productLinkDiv);

        document.getElementById("containerGe").append(productDiv);
        document.getElementById("containerGe").append(salto);
    });
    getRecommendations();
});

String.prototype.capitalize = function () {
    return this.charAt(0).toUpperCase() + this.slice(1);
}

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