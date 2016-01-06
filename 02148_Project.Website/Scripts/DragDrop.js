

dragStart = function(e){
     
    $(this).addClass('drag'); // drag class sets opacity to 0.25
    var dto = e.dataTransfer;
    var sourceContainerId = '';//only want to run script if object is moved
    try {
        dto.setData('text/plain', e.target.id); 
    } catch (ex){
        dto.setData('Text',e.target.id);
    }
    sourceContainerId = this.parentElement.id;
},
dropped= function(e){
    if(this.id !== sourceContainerId){ //only if moving to another drop zone
        cancel(e);
        var id = null,
        dto = e.dataTransfer,
        dropped = null;

        try{
            id= dto.getData('text/plain'); //id of dragged element
        }catch (ex){
            id = dto.getData('Text'); //if internet explorer
        }
        if(id !== null){
            dropped = document.querySelector('#'+id); //append element to droptarget
        }
        if(this.id !== sourceContainerId){
            e.target.appendChild(dropped);
            $(dropped).removeClass('drag');
        }
        $(this).removeClass('over');
	
    }
}
cancel = function(e){
    e.preventDefault();
    e.stopPropagation();
    return false;
}
dragOver = function(e){
    cancel(e);
    $(this).addClass('over');	
}
dragLeave = function(e){
    $(this).removeClass('over');	
}
dragEnd = function(e){
    $('.drag').removeClass('drag');
    $('.over').removeClass('over');
}


var selector = '[data role="drag-drop-container"]',
containers = $(selector);
containers.each(function(index,c){
    c.addEventListener('drop',dropped,false);
    c.addEventListener('dragenter',cancel,false);
    c.addEventListener('dragover',dragOver,false);
    c.addEventListener('dragleave',dragLeave,false);

});
var sources = $('[draggable="true"]'); //all with this attribute

sources.each(function(index,source){
    source.addEventListener('dragstart',dragStart,false);
    source.addEventListener('dragend',dragEnd,false);

});

