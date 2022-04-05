import React from 'react';
import ParentListItem from "./ParentListItem";
import ParentSummary from "./ParentSummary";

/**
 * Shows all groups of parents combining across multiple folders
 */
class MultiFolderProject extends React.Component {

    componentDidMount() {
        this.addFolderParents('X:/Data/SD/practice/Scott/2022/2022-01-04-AON');
        this.addFolderParents('X:/Data/SD/practice/Jordan');
        this.addFolderParents('X:/Data/SD/DSI/CA1/Coronal');
    }

    state = {
        folders: [],
        cells: {},
        selectedParentID: null,
    };

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
        return (
            <div className="text-light p-3 mb-3" style={{ backgroundColor: '#003366' }}>
                <h4>Multi-Folder ABF List</h4>
                <ul>
                    {this.state.folders.map(x => <li key={x}>{x}</li>)}
                </ul>
                <div>
                    <input className='w-50' value={'X:/Data/SD/practice/Scott/2022/some-other-folder/'} readOnly />
                    <button className='ms-2'>Add Folder</button>
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