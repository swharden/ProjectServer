import React from 'react';
import ParentListGroup from "./ParentListGroup";

/**
 * Shows all groups of parents combining across multiple folders
 */
class ParentListFolders extends React.Component {

    componentDidMount() {
        this.addFolderParents('X:/Data/SD/practice/Scott/2022/2022-01-04-AON');
        this.addFolderParents('X:/Data/SD/practice/Jordan');
        this.addFolderParents('X:/Data/SD/DSI/CA1/Coronal');
    }

    state = {
        folders: [],
        parentInfos: {},
    };

    addFolderParents(folderPath) {
        const url = `http://192.168.1.9/abf-browser/api/v3/cells-folder/?folder=` + folderPath;
        fetch(url)
            .then(response => response.json())
            .then(json => {
                const newParentInfos = this.state.parentInfos;
                const newFolders = this.state.folders.concat(json["folder-network"]).sort();
                Object.entries(json["parentInfos"])
                    .forEach(([k, v]) => {
                        newParentInfos[k] = v;
                    });
                this.setState({ parentInfos: newParentInfos, folders: newFolders });
            });
    }

    render() {

        if (this.state.parentInfos.length == 0)
            return <div>Loading...</div>

        const allGroups = Object.entries(this.state.parentInfos).map(([k, v]) => v["group"]);
        const uniqueGroups = [...new Set(allGroups)].sort();

        console.log(this.state.folders);

        return <>
            <div className="alert alert-primary" role="alert">
                <h4 className="alert-heading">Multi-Folder ABF List</h4>
                <ul>
                    {this.state.folders.map(x => <li>{x}</li>)}
                </ul>
            </div>

            {
                uniqueGroups.map(group => <ParentListGroup key={group} group={group} allParentInfos={this.state.parentInfos} />)
            }
        </>
    }
}

export default ParentListFolders;