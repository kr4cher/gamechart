(() => {
    document.addEventListener("DOMContentLoaded", (event) => {
        var d = document.getElementsByClassName("button")[0];
        d.onclick = onButtonClick();
    });

    function onButtonClick() {
        return () => {
            var xhr = new XMLHttpRequest();
            var text = document.getElementsByClassName("text")[0].value;
            //var req = new Request("games", "1479");
            //var text = JSON.stringify(req);
            xhr.open("GET", '/Home/GetOneGame?id=' + text, true);
            xhr.setRequestHeader("Content-type", "application/json");
            xhr.onreadystatechange = (() => {
                if (xhr.readyState == xhr.DONE && xhr.status) {
                    if (xhr.responseText != "" && xhr.responseText != "Invalid customer ID") {
                        var res = JSON.parse(xhr.responseText);
                        var lable = document.getElementsByClassName("lable")[0];
                        lable.innerHTML = res;
                    }
                    else {
                        expandButton.parentElement.parentElement.remove();
                    }
                }
            });
            xhr.send();
        }
    }

    class Request {
        constructor(one, two) {
            this.one_p = one;
            this.two_p = two;
        }
    }

})(); 