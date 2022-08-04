function CopyToClipboard(textToCopy, id = null) {
    console.log("Copying >> " + textToCopy);
    var input = document.createElement("input");
    const element = id ? document.getElementById(id) : document.body;
    element.appendChild(input);
    input.value = textToCopy;
    input.select();
    document.execCommand("copy");
    element.removeChild(input);
}

function CopySetpath(filePath, id = null) {
    CopyToClipboard(`setpath "${filePath}";`, id);
}