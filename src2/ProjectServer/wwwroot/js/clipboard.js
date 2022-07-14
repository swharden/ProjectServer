function CopyToClipboard(textToCopy) {
    var input = document.createElement("input");
    document.body.appendChild(input);
    input.value = textToCopy;
    input.select();
    document.execCommand("copy");
    document.body.removeChild(input);
}

function CopySetpath(filePath) {
    CopyToClipboard(`setpath "${filePath}";`);
}