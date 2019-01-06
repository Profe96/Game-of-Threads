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
    var devices = ["Televisions, Microwaves", "Washing Machine", "Fridges", "ceramic"];

    document.getElementById("deviceHint").innerHTML = "";
    devices.forEach(device => {

        if (device.toLocaleLowerCase().indexOf(str.toLocaleLowerCase()) > -1) {
            document.getElementById("deviceHint").innerHTML = device;
        }

    });
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