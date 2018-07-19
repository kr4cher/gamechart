
(() => {
    document.addEventListener("DOMContentLoaded", (event) => {
        loadGames();
    });

    function loadGames() {
       
            var xhr = new XMLHttpRequest();
            xhr.open("GET", '/Home/GamesByPopularityAsync', true);       //pfad an welche Url die anfrage geschickt wird "request" ist eine variable die an den server weitergegeben wird 
            xhr.setRequestHeader("Content-type", "application/json");
            xhr.overrideMimeType("text/xml");
            xhr.onreadystatechange = (() => {                               // evendlistener bei antwort wird gesetzt
                if (xhr.readyState == xhr.DONE && xhr.status) {             // es wird geschaut ob der server Antwortcode 200 schickt   
                    if (xhr.responseXML!= "") {
                        showGames(xhr.responseXML);            //xhr.response beinhatet die antwort des servers
                        location.reload;
                    }
                    else {
                    }
                }
            });
            xhr.send();                                                     // request wird gesendet                                 
    }

    function loadXMLDoc(filename) {
        if (window.ActiveXObject) {
            xhttp = new ActiveXObject("Msxml2.XMLHTTP");
        }
        else {
            xhttp = new XMLHttpRequest();
        }
        xhttp.open("GET", filename, false);
        try { xhttp.responseType = "msxml-document"; } catch (err) { } // Helping IE11
        xhttp.send("");
        return xhttp.responseXML;
    }

    function showGames(xml) {
        xsl = loadXMLDoc("../Scripts/displayGames.xsl");
        // Für Internet Explorer
        if (window.ActiveXObject || xhttp.responseType === "msxml-document") {
            ex = xml.transformNode(xsl);
            document.getElementById("gameDiv").innerHTML = ex;
        }
        //Für zeitgemäße Browser
        else if (document.implementation && document.implementation.createDocument) {
            xsltProcessor = new XSLTProcessor();
            xsltProcessor.importStylesheet(xsl);
            resultDocument = xsltProcessor.transformToFragment(xml, document);
            document.getElementById("gameDiv").appendChild(resultDocument);
        }
        var gameButton = document.getElementsByClassName("game-button");
        for (var i = 0; i < gameButton.length; i++) {
            var id = gameButton[i].id.split(":")[1];
            gameButton[i].onclick = onclicktoggle(id);
        }
    }

    function onclicktoggle(id) {
        return () => {
                toggleModal(id);
        };
    }

    var el;
    function toggleModal(Id) {
        if (el != null && el != document.getElementById(Id)) {
            el.style.visibility = "hidden";
        }
        el = document.getElementById(Id);
        el.style.visibility = (el.style.visibility == "visible") ? "hidden" : "visible";
        var button = el.getElementsByClassName("modal-button")[0];
        button.onclick = (() => {
            closeModal(Id);
        });
    }
    function closeModal(Id) {
        el.style.visibility = (el.style.visibility == "visible") ? "hidden" : "visible";
    }

    function showInfo(ID) {
        alert(ID);
    }
})(); 