class AbfCellData {
    constructor(path, color, comment, group) {
        this.path = String(path).replace("\\", "/");
        this.color = color;
        this.comment = comment;
        this.group = group;
        this.childAbfs = [];
        this.analyses = [];
        this.abfID = String(path).replace("\\", "/").split("/").pop().replace(".abf", "");
        this.folder = this.path.replace("/" + this.abfID + ".abf", "");
    }
}

export default AbfCellData;