var userEmail = null;

$(() => {
    $("#submitButton").on("click", () => {
        var url_string = window.location.href
        var url = new URL(url_string);
        var c = url.searchParams.get("search");
        var d = serializeSchema(document.getElementById("filterFormTec"));
        c += " " + parseData(d);
        $('body').loadingModal({         
              text: 'Loading...',
              color: '#6DBA3A',
              opacity: '0.7',
              backgroundColor: 'rgb(0,0,0)',
              animation: 'foldingCube'
            
            });
        $.ajax({
            url: serverApi + "product?searchTerm=" + c,
            success: function (result) {
                $('body').loadingModal('destroy');
                window.sessionStorage.setItem('products', JSON.stringify(result));
                window.location = "./landing.html"
            }
        });
    });

    if (getCookie('email') !== "") {
        document.getElementById('GoogleAuthLogIn').hidden = true;
        document.getElementById('GoogleAuthLogOut').hidden = false;
    }
});

function parseData(data) {
    let s = {};
    data.forEach(da => {
        var id = da.id;
        if (da.check) {
            if (da.id == "black" || da.id == "white" || da.id == "gray") {
                s.color = da.id;
            }
            if (da.id == "hd" || da.id == "fullhd" || da.id == "4k") {
                s.tech = da.id;
            }
            if (da.id == "led" || da.id == "plasma" || da.id == "lcd" || da.id == "oled") {
                s.tech2 = da.id;
            }
        } else {
            s[id] = da.value;
        }
    });
    s.screenSize = (s.screenSize) ? s.screenSize + "\" " : "";
    s.desiredPricing = (s.desiredPricing) ? "$" + s.desiredPricing + " " : "";

    return s.screenSize + s.desiredPricing + s.prefferedBrand + " " + s.screenTech + ((s.color) ? " " + s.color : "") +
        ((s.tech) ? " " + s.tech : "") + ((s.tech2) ? " " + s.tech2 : "");
}

function onSignIn(googleUser) {
    var id_token = googleUser.getAuthResponse().id_token;
    $.ajax({
        url: serverApi + "google/auth?idToken=" + id_token,
        success: function (result) {
            setCookie('id', result.id, 1);
            document.getElementById('GoogleAuthLogIn').hidden = true;
            document.getElementById('GoogleAuthLogOut').hidden = false;
        }
    });
}

function filtro(clave, valor) {
    if (valor == '' || clave == null) {
        return undefined;
    } else {
        return valor;
    }
};


function selectionClickHandler(product) {
    var email = getCookie('email');
    if (email) {
        $.ajax({
            url: serverApi + "selected?" +
                "email=" + email +
                "&id=" + product.id +
                "&link=" + product.link +
                "&description=" + product.description,
            success: function () {
                console.log("Saved.")
            }
        });
    }
};

function signOut() {
    var auth2 = gapi.auth2.getAuthInstance();
    auth2.signOut().then(function () {
        setCookie('id', '', 1);
        document.getElementById('GoogleAuthLogIn').hidden = false;
        document.getElementById('GoogleAuthLogOut').hidden = true;
    });
}

const slider = () => {

    var slider = document.getElementById("myRange");
    var output = document.getElementById("value");
    output.innerHTML = slider.value;

    slider.oninput = function () {
        output.innerHTML = this.value;
        output.innerHTML += " €";
    }
};


const chequeado = (value) => {
    console.log("checked called");
    console.log(value);
    document.getElementById('wantedThick').disabled = !value;
};




