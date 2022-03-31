class AbfParent {
    constructor(fullPath) {
        this.color = "#00FF00";
        this.comment = "no comment";
        this.childAbfs = [];
        this.childImages = [];
        this.path = fullPath;
        this.filename = fullPath.replace(/^.*[\\\\/]/, ''); // isolate filename
        this.abfID = this.filename.replace(/\.[^/.]+$/, ""); // remove extension
    }
}

export default AbfParent;