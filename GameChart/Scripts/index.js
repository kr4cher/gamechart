(() => {
    document.addEventListener("DOMContentLoaded", (event) => {
        showGames();
        var apiButton = document.getElementsByClassName("namedesButtons")[0]; //name das buttons
        apiButton.onclick = sendRequest();
    });

    function sendRequest() {
        return () => {
            var xhr = new XMLHttpRequest();
            xhr.open("GET", '/Home/APIAnswer?call=' + "das was du haben willst", true);       //pfad an welche Url die anfrage geschickt wird "request" ist eine variable die an den server weitergegeben wird 
            xhr.setRequestHeader("Content-type", "application/json");
            xhr.onreadystatechange = (() => {                               // evendlistener bei antwort wird gesetzt
                if (xhr.readyState == xhr.DONE && xhr.status) {             // es wird geschaut ob der server Antwortcode 200 schickt   
                    if (xhr.responseText != "") {
                        var res = JSON.parse(xhr.responseText);             //xhr.response beinhatet die antwort des servers
                    }
                    else {
                    }
                }
            });
            xhr.send();                                                     // request wird gesendet
        }                                                  
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

    function showGames() {
        xml = loadXMLDoc("../Scripts/games.xml");
        xsl = loadXMLDoc("../Scripts/allGames.xsl");
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
    }
})(); 