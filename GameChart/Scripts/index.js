
(() => {
    document.addEventListener("DOMContentLoaded", (event) => {
        var button = document.getElementsByClassName("download")[0];
        button.onclick = download;
        loadGames();
    });

    const monthNames = ["January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"
    ];

    var XMLFile;

    function loadGames() {
        var xhr = new XMLHttpRequest();
        xhr.open("GET", '/Home/GamesByPopularityAsync', true);       		//pfad an welche Url die anfrage geschickt wird "request" ist eine variable die an den server weitergegeben wird 
        xhr.setRequestHeader("Content-type", "application/json");
        xhr.overrideMimeType("text/xml");
        xhr.onreadystatechange = (() => {                               // evendlistener bei antwort wird gesetzt
            if (xhr.readyState == xhr.DONE && xhr.status) {             // es wird geschaut ob der server Antwortcode 200 schickt   
                if (xhr.responseXML != "") {
                    showGames(xhr.responseXML);            					//xhr.response beinhatet die antwort des servers
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

    function download() {
        var isIE = /*@cc_on!@*/false || !!document.documentMode;
        
        var isEdge = !isIE && !!window.StyleMedia;

        if (isEdge) {
            var xmlContent = XMLFile;
            var blob = new Blob([xmlContent], {
                type: "application/xml"
            });
            window.navigator.msSaveOrOpenBlob(blob, "GameChart" + ".xml");
        }
        else {
            var element = document.createElement('a');
            element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(XMLFile));
            element.setAttribute('download', "GameChart.xml");

            element.style.display = 'none';
            document.body.appendChild(element);
            element.click();

            document.body.removeChild(element);
        }
    }

    function showGames(xml) {
        XMLFile = new XMLSerializer().serializeToString(xml.documentElement);
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
        //click event
        var gameButton = document.getElementsByClassName("game-button");
        for (var i = 0; i < gameButton.length; i++) {
            var id = gameButton[i].id.split(":")[1];
            gameButton[i].onclick = onclicktoggle(id);
        }
        //Datum Format anpassen
        var releasedates = document.getElementsByClassName("ReleaseDate");
        for (var j = 0; j < releasedates.length; j++) {
            var ticks = releasedates[j].innerHTML;
            if (parseInt(ticks) > 0) {
                var date = new Date(parseInt(ticks));
                releasedates[j].innerHTML = monthNames[date.getMonth() - 1] + " " + date.getFullYear();
            }
            else {
                releasedates[j].innerHTML = "No releasedate set";
            }
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