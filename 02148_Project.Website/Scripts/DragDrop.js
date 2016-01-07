

function allowDrop(ev) {
    ev.preventDefault();
}

function drag(ev) {
    if (ev.target.parentNode.nodeName == "DIV") {
        ev.target.parentNode.removeChild(ev.target.parentNode.lastChild);
    }
    ev.dataTransfer.setData("text", ev.target.id);
    ev.dataTransfer.setData("text/html", ev.target.dataset.price);
}

function drop(ev) {
    console.log("cid " + ev.target.id);
    if (ev.target.nodeName == "DIV") {
        ev.preventDefault();

        var id = ev.dataTransfer.getData("text");
        var price = ev.dataTransfer.getData("text/html");
        console.log("text: " + id);
        console.log("price: " + price);
        var img = document.getElementById(id);
        var newChild = document.createElement("div");
        var para = document.createElement("p");
        para.className = "pricetext";
        para.textContent = "price: " + price;
        newChild.appendChild(img);
        newChild.appendChild(para);
        ev.target.appendChild(newChild);
        
    } else {
        ev.preventDefault();
        var price = ev.dataTransfer.getData("text/html");
        var para = document.createElement("p");
        para.className = "pricetext";
        para.textContent = "price: " + price;
        ev.target.parentNode.appendChild(para);

    }


    
}

