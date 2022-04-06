import React from 'react';
import ParentListItem from "./ParentListItem";
import ParentSummary from "./ParentSummary";

// TODO: left menu should scroll
// TODO: indicate selected parent in menu

class MultiFolderProject extends React.Component {

    state = {
        project: null, // TODO: put project viewer/editor into its own component
        projectEditable: false,
        folders: [],
        cells: {},
        selectedParentID: null,
    };

    componentDidMount() {
        const projFilePath = 'X:/Users_Public/Scott/temp/example.abfproj';
        const url = `http://192.168.1.9/abf-browser/api/v3/project/?path=` + projFilePath;
        fetch(url)
            .then(response => response.json())
            .then(json => {
                this.setState({ project: json });
                this.addFolders(json["abfFoldersScanned"]);
            });
    }

    addFolders(abfFolders) {
        abfFolders.forEach(folderPath => {
            if (String(folderPath).includes("*")) {
                //TODO: if folder path ends with * do something special
            } else {
                this.addFolderParents(folderPath);
            }
        });
    }

    addFolderParents(folderPath) {
        const url = `http://192.168.1.9/abf-browser/api/v3/cells-folder/?folder=` + folderPath;
        fetch(url)
            .then(response => response.json())
            .then(json => {
                const newCells = this.state.cells;
                const newFolders = this.state.folders.concat(json["folderPathNetwork"]).sort();
                Object.entries(json["cells"])
                    .forEach(([k, v]) => {
                        newCells[k] = v;
                    });
                this.setState({ cells: newCells, folders: newFolders });
            });
    }

    getGroupWithABFs(group) {
        const matchingCells = Object.entries(this.state.cells)
            .filter(x => x[1]["group"] === group)
            .sort()
        return (
            <div key={group}>
                <h3 className='mt-4'>{group ?? "No Group Defined"}</h3>
                {
                    matchingCells.map(([k, v]) =>
                        <ParentListItem key={k} cell={v}
                            onClick={() => this.setState({ selectedParentID: k })} />
                    )
                }
            </div>
        );
    }

    getHeader() {

        const title = this.state.project ? this.state.project.title : "Multi-Folder Loaded";
        const subtitle = this.state.project ? this.state.project.subtitle : "No Project Loaded";

        return (
            <div className="text-light p-3" style={{ backgroundColor: '#003366' }}>
                <div>
                    <a href='#' className='text-light text-decoration-none fs-3'
                        onClick={() => this.setState({ selectedParentID: null })}>{title}</a>
                </div>
                <div className='' style={{ opacity: .5 }}>
                    <a href='#' className='text-light text-decoration-none'>{subtitle}</a>
                </div>
            </div >
        );
    }

    getMenu() {

        const allGroups = Object.entries(this.state.cells).map(([k, v]) => v["group"]);
        const uniqueGroups = [...new Set(allGroups)].sort();

        const selectedCell = this.state.cells[this.state.selectedParentID];

        return <>
            {uniqueGroups.map(group => this.getGroupWithABFs(group))}
        </>
    }

    getProjectDetailsEditable() {

        const project = this.state.project;

        if (project == null)
            return <>no projected loaded...</>

        return <form>
            <div className='p-3'>
                <h1 className='mb-0'>Project Editor</h1>
                <div className='mb-3'>
                    <code>{project.path}</code>
                </div>
                <div className='my-2'>
                    <div className='form-label'>Title</div>
                    <input className='form-control'
                        defaultValue={project.title} />
                </div>
                <div className='my-2'>
                    <div className='form-label'>Subtitle</div>
                    <input className='form-control'
                        defaultValue={project.subtitle} />
                </div>
                <div className='my-2'>
                    <div className='form-label'>Notes</div>
                    <textarea className='form-control' rows='7'
                        defaultValue={project.notes} />
                </div>
                <div className='my-2'>
                    <div className='form-label'>Paths</div>
                    <textarea className='form-control' rows='7'
                        defaultValue={project.abfFolders.join("\n")} />
                </div>
                <div className='text-end my-4'>
                    <input type="reset" className="btn btn-outline-secondary ms-3" value="Reset" />
                    <button type="submit" className="btn btn-outline-secondary ms-3"
                        onClick={() => this.setState({ projectEditable: false })}>Cancel</button>
                    <button type="submit" className="btn btn-primary ms-3"
                        onClick={() => this.setState({ projectEditable: false })}>Save</button>
                </div>
            </div>
        </form>
    }

    getProjectDetailsReadonly() {

        const project = this.state.project;

        if (project == null)
            return <>no projected loaded...</>

        return (
            <>
                <form>
                    <div className='p-3'>
                        <h1 className='mb-0'>{project.title}</h1>
                        <div className='fs-4'>{project.subtitle}</div>

                        <div className='mt-4'>Notes:</div>
                        <div className='ms-3'>
                            {project.notes}
                        </div>

                        <div className='mt-4'>Paths:</div>
                        <div className='ms-3'>
                            {project.abfFolders.map((x) => <div key={x}><code>{x}</code></div>)}
                        </div>

                        <div className='text-end my-4'>
                            <button type="submit" className="btn btn-primary ms-3"
                                onClick={() => this.setState({ projectEditable: true })}>Edit</button>
                        </div>
                    </div>
                </form>
            </>
        )
    }

    getSelectedCell() {
        const selectedCell = this.state.cells[this.state.selectedParentID];

        if (selectedCell == null)
            return this.state.projectEditable
                ? this.getProjectDetailsEditable()
                : this.getProjectDetailsReadonly();

        return <>
            <ParentSummary key={selectedCell} cell={selectedCell} />
        </>
    }

    getFooter() {
        const folders = this.state.project ? this.state.project.abfFolders : [];
        const abfProjectPath = this.state.project ? this.state.project.path : "not loaded...";

        return <div className='bg-dark text-muted text-light mt-5 p-4'>
            <div>ABF Project File:</div>
            <ul>
                <li>{abfProjectPath}</li>
            </ul>
            <div>Folders represented:</div>
            <ul>
                {folders.map(x => <li key={x}>{x}</li>)}
            </ul>
            <div>Abf Browser API: <a href='http://192.168.1.9/abf-browser/api/v3/'>Version 3</a></div>
        </div >
    }

    render() {

        if (this.state.cells.length === 0)
            return <div>Loading...</div>

        return <>
            <div className='row'>
                {this.getHeader()}
            </div>
            <div className='row'>
                <div className='col-3 border bg-light rounded'>
                    {this.getMenu()}
                </div>
                <div className='col'>
                    {this.getSelectedCell()}
                </div>
            </div>
            <div className='row'>
                {this.getFooter()}
            </div>
        </>
    }
}

export default MultiFolderProject;