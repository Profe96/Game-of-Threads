function getRecommendations() {
    var email = getCookie('id');
    if (email) {
        $.ajax({
            async: true,
            url: serverApi + "product/recommendation?id=" + email,
            success: function (result) {
                attachRecommendationsToView(result)
            }
        });
    }
}

function attachRecommendationsToView(result) {
    var d = document.getElementById("recome");

    result.forEach(g => {
        createCardRe(g);
    });

    document.getElementById("res").removeAttribute("hidden");

    function createCardRe(g) {
        var recomm = document.createElement('div');
        recomm.className = "col-4";
        recomm.style = "cursor:pointer;background-color: white;padding:10px;border-shadow:5px;box-shadow: 0px 10px 20px -15px rgba(0,0,0,0.75), 0px -10px 20px -15px rgba(0,0,0,0.75);";
        recomm.onclick = () => { selectionFromUser(g.link, g.id, g.description) }

        var image = document.createElement('img');
        image.src = g.imageUrl;
        image.style = "display:block;margin-left:auto;margin-right:auto;max-height:100px;"
        recomm.append(image);

        var title = document.createElement('h4');
        title.innerHTML = g.name.substr(0, 22) + "...";
        title.style = "display:block;margin-left:auto;margin-right:auto;text-align:center;font-family: Montserrat;font-size:125%;"
        recomm.append(title);

        var price = document.createElement('p');
        price.innerHTML = g.price;
        price.style = "text-align:center; font-size:100%; color:rgb(109, 186, 58);"
        recomm.append(price);

        d.append(recomm);
    }
}