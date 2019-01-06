const serverApi = "https://gameofthreads.azurewebsites.net/";
var userEmail = null;

$(() => {
    $("#submitButton").on("click", () => {
        var url_string = window.location.href
        var url = new URL(url_string);
        var c = url.searchParams.get("search");
        console.log(serializeSchema(document.getElementById("filterFormTec")));
        $('body').loadingModal({
            
              text: 'Loading...',
              color: '#6DBA3A',
              opacity: '0.7',
              backgroundColor: 'rgb(0,0,0)',
              animation: 'foldingCube'
            
            });

        //al terminar la llamada ajax instanciar esta linea
        $('body').loadingModal('destroy');

        /*
        $.ajax({
        url: serverApi + "product?searchTerm=" + c,
        success: function (result) {
        window.sessionStorage.setItem('products', JSON.stringify(result));
        window.location = "./landing.html"
        }
        });
        */

//


//
        
    });

    if (getCookie('email') !== "") {
        document.getElementById('GoogleAuthLogIn').hidden = true;
        document.getElementById('GoogleAuthLogOut').hidden = false;
    }
});

function onSignIn(googleUser) {
    var id_token = googleUser.getAuthResponse().id_token;
    $.ajax({
        url: serverApi + "google/auth?idToken=" + id_token,
        success: function (result) {
            setCookie('email', result.email, 0.005);
            document.getElementById('GoogleAuthLogIn').hidden = true;
            document.getElementById('GoogleAuthLogOut').hidden = false;
        }
    });
}

function getRecommendations() {
    var email = getCookie('email');
    if (email) {
        $.ajax({
            url: serverApi + "product/recommendation?email=" + email,
            success: function (result) {
                console.log(result);
            }
        });
    }
}

function filtro(clave, valor) {
    if (valor == '' || clave == null) {
        return undefined;
    } else {
        return valor;
    }
};

function serializeSchema(form) {
    let array = [].map.call(form.getElementsByTagName("*"), function (el) {
        switch (el.tagName) {
            case 'INPUT':
            switch(el.type){
                    case 'checkbox':
                    return (el.checked) ? {
                        id: el.id,
                        check: el.checked,
                    }:null
                    default:
                    return{
                        id: el.id,
                        value: el.value,
                    }

            }                    
              

        }
    }).filter(function (e) { return e !== undefined; });
    return array.filter(Boolean);
}

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
}

function signOut() {
    var auth2 = gapi.auth2.getAuthInstance();
    auth2.signOut().then(function () {
        setCookie('email', '', 0.005);
        document.getElementById('GoogleAuthLogIn').hidden = false;
        document.getElementById('GoogleAuthLogOut').hidden = true;
    });
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

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
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

const showHint = (str) => {
    if (str.length == 0) {
        document.getElementById("brandHint").innerHTML = "";
        return;
    } else {
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                document.getElementById("brandHint").innerHTML = this.responseText;
            }
        };
        xmlhttp.open("GET", "getHintBrands.php?q=" + str, true);
        xmlhttp.send();
    }
};




const verifyBrands = (str) => {

    // var input = Document.getElementById(prefferedBrand).value;
    
    var brands = ["Sony", "LG", "Philips", "Noblex", "TLC", "RCA", "Hitachi", "Panasonic"];
    document.getElementById("brandHint").innerHTML = "";

    brands.forEach(brand => {
        if (brand.toLowerCase().indexOf(str.toLowerCase()) > -1) {
            document.getElementById("brandHint").innerHTML = brand;
        }
    });


};


const verifyDevices = (str) => {
    var devices = ["Televisores, Microondas", "Lavadoras", "Refrigeradoras", "Microondas", "Vitroceramicas", "Cocinas"];

    document.getElementById("deviceHint").innerHTML = "";
    devices.forEach(device => {

        if (device.toLocaleLowerCase().indexOf(str.toLocaleLowerCase()) > -1) {
            document.getElementById("deviceHint").innerHTML = device;
        }

    });
};



