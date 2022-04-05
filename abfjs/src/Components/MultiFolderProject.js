import React from 'react';
import ParentListItem from "./ParentListItem";
import ParentSummary from "./ParentSummary";

/**
 * Shows all groups of parents combining across multiple folders
 */
class MultiFolderProject extends React.Component {

    state = {
        project: null,
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
                this.addFolders(json["abfFolders"]);
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

    getFolderListEditor() {

        const title = this.state.project ? this.state.project.title : "Multi-Folder Loaded";
        const subtitle = this.state.project ? this.state.project.subtitle : "No Project Loaded";
        const projPath = this.state.project ? this.state.project.path : "No Project Loaded";

        return (
            <div className="text-light p-3 mb-3" style={{ backgroundColor: '#003366' }}>
                <h2>{title}</h2>
                <div>{subtitle}</div>
                <div><code className='text-muted'>{projPath}</code></div>
                <ul>
                    {this.state.folders.map(x => <li key={x}>{x}</li>)}
                </ul>
                <div>
                    <button className='ms-2'>Add Folder(s)</button>
                    <button className='ms-2'>Edit Title/Description</button>
                    <button className='ms-2'>Edit Notes</button>
                </div>
            </div>
        );
    }

    getMenu() {

        const allGroups = Object.entries(this.state.cells).map(([k, v]) => v["group"]);
        const uniqueGroups = [...new Set(allGroups)].sort();

        const selectedCell = this.state.cells[this.state.selectedParentID];

        return <>
            <div className='col-3 border bg-light rounded'>
                <div className='mt-3'>
                    <strong>Electrophysiology Project</strong>
                </div>
                <div>
                    ABFs combined across {this.state.folders.length} folders
                </div>
                <hr />
                {uniqueGroups.map(group => this.getGroupWithABFs(group))}
            </div>
            <div className='col'>
                <ParentSummary key={selectedCell} cell={selectedCell} />
            </div>
        </>
    }

    render() {

        if (this.state.cells.length === 0)
            return <div>Loading...</div>

        return <>
            <div className='row'>
                {this.getFolderListEditor()}
            </div>
            <div className='row'>
                {this.getMenu()}
            </div>
        </>
    }
}

export default MultiFolderProject;