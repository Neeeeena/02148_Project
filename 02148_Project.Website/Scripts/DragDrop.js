

function allowDrop(ev) {
    ev.preventDefault();
}

function drag(ev) {
    ev.dataTransfer.setData("text/id", ev.target.id);

}

function drop(ev) {

    ev.preventDefault();

    var id = ev.dataTransfer.getData("text/id");
    var img = document.getElementById(id);
    ev.target.appendChild(img);
    console.log("ID " + id);
    var hid = document.getElementById("test");
    hid.setAttribute("value", id);
    console.log("HID " + hid.value);


    
}

