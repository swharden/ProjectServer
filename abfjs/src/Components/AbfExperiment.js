import AbfCellData from '../Models/AbfCellData';
import AbfExperimentData from '../Models/AbfExperimentData';
import React from 'react';

class AbfExperiment extends React.Component {

    state = {
        experiment: new AbfExperimentData(null),
        cells: [],
        viewByGroup: true,
    }

    componentDidMount() {
        console.clear();

        const devUrlsIntrinsic = [
            "http://localhost/sample-data/abfday/exp1/2021-11-08.json",
            "http://localhost/sample-data/abfday/exp1/2021-11-09.json",
            "http://localhost/sample-data/abfday/exp1/2021-11-10.json",
            "http://localhost/sample-data/abfday/exp1/2021-11-20.json",
            "http://localhost/sample-data/abfday/exp1/2021-11-22.json",
            "http://localhost/sample-data/abfday/exp1/2021-11-23.json",
        ]

        const devUrlsTimeCourse = [
            "http://localhost/sample-data/abfday/exp2/2021-05-03.json",
            "http://localhost/sample-data/abfday/exp2/2021-05-04.json",
            "http://localhost/sample-data/abfday/exp2/2021-05-06.json",
            "http://localhost/sample-data/abfday/exp2/2021-05-10.json",
            "http://localhost/sample-data/abfday/exp2/2021-05-11.json",
            "http://localhost/sample-data/abfday/exp2/2021-05-17.json",
            "http://localhost/sample-data/abfday/exp2/2021-07-30.json",
        ]

        const devUrlsCA1 = [
            "http://localhost/sample-data/abfday/exp3/2022-03-24.json",
            "http://localhost/sample-data/abfday/exp3/2022-04-01.json",
            "http://localhost/sample-data/abfday/exp3/2022-04-04.json",
            "http://localhost/sample-data/abfday/exp3/2022-04-05.json",
            "http://localhost/sample-data/abfday/exp3/2022-04-06.json",
        ]

        devUrlsCA1.forEach(url => { this.addAbfDay(url) });
    }

    addAbfDay(url) {
        fetch(url)
            .then(response => response.json())
            .then(json => {
                const fullCellList = this.state.cells;
                const thisDayCells = json.cells;
                if (!thisDayCells)
                    return;
                thisDayCells.forEach(x => {
                    const abfFilePath = json.path + "/" + x.id + ".abf";
                    const newCell = new AbfCellData(abfFilePath, x.color, x.comment, x.group);
                    fullCellList.push(newCell);
                })
                this.setState({ cells: fullCellList });
            });
    }

    renderCellLi(cell, showGroup) {
        return <li key={cell.abfID}>
            <span style={{ backgroundColor: cell.color, padding: "0px 3px", marginRight: ".2em" }}>
                <a href='' className='font-monospace' style={{ fontSize: ".8em" }}>{cell.abfID}</a>
            </span>
            <span style={{ display: (cell.group && showGroup) ? "visible" : "none" }}>[{cell.group}]</span>
            <span>{cell.comment}</span>
        </li>
    }

    renderCellsUnderHeading(heading, cells, showGroupInline) {
        return <div key={heading}>
            <div className='ps-1 fw-bold'>{heading}</div>
            <ul className='ps-2' style={{listStyleType: 'none'}}>
                {
                    cells.map(cell => this.renderCellLi(cell, showGroupInline))
                }
            </ul>
        </div>
    }

    renderCellsByDay() {
        const folders = this.state.cells.map(x => x.folder);
        const uniqueFolders = [...new Set(folders)];
        return uniqueFolders.map(folder => {
            const folderName = folder.split("/").pop();
            const folderCells = this.state.cells.filter(x => x.folder == folder);
            return this.renderCellsUnderHeading(folderName, folderCells);
        });
    }

    renderCellsByGroup() {
        const groups = this.state.cells.map(x => x.group);
        const uniqueGroups = [...new Set(groups)];
        return uniqueGroups.map(group => {
            const groupCells = this.state.cells.filter(x => x.group == group);
            return this.renderCellsUnderHeading(group, groupCells);
        });
    }

    render() {
        return (
            <>
                <div className='p-2' style={{ backgroundColor: '#DDD' }}>
                    <div>ABF Experiment</div>
                    <div>
                        <button className='btn btn-primary btn-sm me-2' onClick={() => { this.setState({ viewByGroup: true }) }}>by group</button>
                        <button className='btn btn-primary btn-sm me-2' onClick={() => { this.setState({ viewByGroup: false }) }}>by day</button>
                    </div>
                </div>
                {this.state.viewByGroup ? this.renderCellsByGroup() : this.renderCellsByDay()}
            </>
        );
    }
}

export default AbfExperiment;
