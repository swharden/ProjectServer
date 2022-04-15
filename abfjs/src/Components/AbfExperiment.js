import AbfCellData from '../Models/AbfCellData';
import AbfDay from '../Models/AbfDay';
import AbfParentSummary from './AbfParentSummary';
import React from 'react';

class AbfExperiment extends React.Component {

    state = {
        abfDays: [],
        cells: [],
        selectedCell: null,
        viewByGroup: false,
        groups: [],
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

        const localPathsCA1 = [
            'X:/Projects/Aging-eCB/experiments/DSI in CA1/2022-03-24',
            'X:/Projects/Aging-eCB/experiments/DSI in CA1/2022-04-01',
            'X:/Projects/Aging-eCB/experiments/DSI in CA1/2022-04-04',
            'X:/Projects/Aging-eCB/experiments/DSI in CA1/2022-04-05',
            'X:/Projects/Aging-eCB/experiments/DSI in CA1/2022-04-08',
        ]

        const localUrlsCA1 = localPathsCA1.map(x => 'http://192.168.1.9/abf-browser/api/v4/abfday/?path=' + x)

        localUrlsCA1.forEach(url => { this.addAbfDay(url) });
    }

    getAbfsByParent(filenames) {
        const abfFilenames = filenames.filter(x => String(x).endsWith(".abf"));
        const tifFilenames = filenames.filter(x => String(x).endsWith(".tif"));
        const abfParentFilenames = abfFilenames.filter(abfFilename => tifFilenames.includes(abfFilename.replace(".abf", ".tif")))

        let parent = "orphan"
        const abfsByParent = {};
        for (let i = 0; i < abfFilenames.length; i++) {
            if (abfParentFilenames.includes(abfFilenames[i]))
                parent = abfFilenames[i].replace(".abf", "");
            if (!Object.keys(abfsByParent).includes(parent))
                abfsByParent[parent] = []
            abfsByParent[parent].push(abfFilenames[i]);
        }

        return abfsByParent;
    }

    addAbfDay(url) {
        fetch(url)
            .then(response => response.json())
            .then(json => {
                const fullCellList = this.state.cells;
                const thisDayCells = json.cells;
                const abfsByParent = this.getAbfsByParent(json.files)
                const groupsList = this.state.groups;

                if (!thisDayCells)
                    return;

                thisDayCells.forEach(x => {
                    const abfFilePath = json.path + "/" + x.id + ".abf";
                    const newCell = new AbfCellData(abfFilePath, x.color, x.comment, x.group);
                    newCell.childAbfs = abfsByParent[x.id];
                    fullCellList.push(newCell);
                    if (!groupsList.includes(x.group))
                        groupsList.push(x.group);
                })

                groupsList.sort();

                const abfDaysList = this.state.abfDays;
                const abfday = new AbfDay(json.path);
                fullCellList.forEach(cell => abfday.cells.push(cell));
                abfDaysList.push(abfday);

                this.setState({ cells: fullCellList, abfDays: abfDaysList, groups: groupsList });
            });
    }

    renderCellLi(cell, showGroup) {

        const groupLabel = <div className='d-inline-block badge rounded-pill bg-secondary'
            style={{ fontSize: ".8em", marginRight: ".2em" }}>{cell.group}</div>;

        const visibleGroupLabel = cell.group && showGroup ? groupLabel : null;

        return <li key={cell.abfID}>
            <span style={{ backgroundColor: cell.color, padding: "0px 3px", marginRight: ".2em" }}>
                <a onClick={() => { this.setState({ selectedCell: cell }); }} className='font-monospace' style={{ fontSize: ".8em" }}>{cell.abfID}</a>
            </span>

            {visibleGroupLabel}

            <div className='d-inline-block'
                style={{ fontSize: ".8em", marginRight: ".2em", opacity: .5 }}>{cell.comment}</div>
        </li>
    }

    renderCellsByDay() {
        return this.state.abfDays.map(abfDay => {
            const folderName = abfDay.path.split("/").pop();
            return (
                <div key={folderName}>
                    <div className='ps-1 fw-bold'>{folderName}</div>
                    <ul className='ps-2' style={{ listStyleType: 'none' }}>
                        <li>Path: <code>{abfDay.path}</code></li>
                        <li>Operator: {abfDay.operator}</li>
                        <li>Animal: {abfDay.animal}</li>
                        <li>Bath: {abfDay.bath}</li>
                        <li>Drugs: {abfDay.drugs}</li>
                        <li>Internal: {abfDay.internal}</li>
                        <li>Notes: {abfDay.notes}</li>
                        <li>Cells ({abfDay.cells.length}):</li>
                        {abfDay.cells.map(cell => this.renderCellLi(cell, true))}
                    </ul>
                </div>
            )
        });
    }

    renderCellsByGroup() {
        const groups = this.state.cells.map(x => x.group);
        const uniqueGroups = [...new Set(groups)];
        return uniqueGroups.map(group => {
            const groupCells = this.state.cells.filter(x => x.group == group);
            return (
                <div key={group}>
                    <div className='ps-1 fw-bold'>{group}</div>
                    <ul className='ps-2' style={{ listStyleType: 'none' }}>
                        {
                            groupCells.map(cell => this.renderCellLi(cell, false))
                        }
                    </ul>
                </div>
            )
        });
    }

    render() {
        return (
            <>
                <div className='row' style={{ backgroundColor: '#DDD' }} >
                    <div className='mx-2 mb-2' >
                        <div>ABF Experiment</div>
                        <div>
                            <button className='btn btn-primary btn-sm me-2' onClick={() => { this.setState({ viewByGroup: true }) }}>by group</button>
                            <button className='btn btn-primary btn-sm me-2' onClick={() => { this.setState({ viewByGroup: false }) }}>by day</button>
                        </div>
                    </div>
                </div>
                <div className='row'>
                    <div className='col-4 border'>
                        {
                            this.state.viewByGroup
                                ? this.renderCellsByGroup()
                                : this.renderCellsByDay()
                        }
                    </div>
                    <div className='col'>
                        <AbfParentSummary cell={this.state.selectedCell} groups={this.state.groups} />
                    </div>
                </div>
            </>
        );
    }
}

export default AbfExperiment;
