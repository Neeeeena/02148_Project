

function allowDrop(ev) {
    ev.preventDefault();
}

function drag(ev) {
    ev.dataTransfer.setData("text", ev.target.id);
}

function drop(ev) {
    console.log("cid " + ev.target.id);
    if (ev.target.nodeName == "DIV") {
        ev.preventDefault();
        var data = ev.dataTransfer.getData("text");
        ev.target.appendChild(document.getElementById(data));
        
    } else {
        ev.preventDefault();


    }


    
}

