import React from 'react';
import AbfCellData from '../Models/AbfCellData';
import AbfInfoLine from "./AbfInfoLine";

class AbfParentSummary extends React.Component {

    getFilenameWithoutExtension(x) {
        return x.replace(/^.*[\\/]/, '').replace(/\.[^/.]+$/, "");
    }

    render() {

        const cell = this.props.cell;

        if (cell == null)
            return <div>no parent selected</div>

        return <>
            <div className='p-2 border border-dark' style={{ backgroundColor: cell.color }}>
                <h1>{cell.abfID}</h1>
                <div className='font-monospace mb-2' style={{ opacity: .2 }}>{cell.folder}</div>
                <input value={cell["comment"]} className="w-50" readOnly />
                <button className='ms-2'>Submit</button>

                <span className='mx-2'>Group:</span>
                <select name="cars" id="cars">
                    {this.props.groups.map(x => <option value={x} selected={x == cell.group}>{x}</option>)}
                </select>
            </div>

            <div className='bg-light border'>
                {
                    cell.childAbfs.map(x =>
                        <div className='my-2 px-1' key={x}>{this.getFilenameWithoutExtension(x)}
                            <button className='ms-1 btn btn-primary btn-sm' style={{ fontSize: '.7em' }}>Copy</button>
                            <button className='ms-1 btn btn-success btn-sm' style={{ fontSize: '.7em' }}>SetPath</button>
                            <button className='ms-1 btn btn-danger btn-sm' style={{ fontSize: '.7em' }}>Ignore</button>
                            <div className='ps-1 d-inline'
                                style={{ fontSize: '.8em', fontFamily: 'arial narrow' }}>
                                <AbfInfoLine abfPath={x} />
                            </div>
                        </div>
                    )
                }
            </div>

            <div>
                {
                    cell.analyses.map(x =>
                        <a href={x} key={x}>
                            <img className='m-3 shadow border border-dark' alt='' style={{ maxHeight: '200px' }} src={x} />
                        </a>
                    )
                }
            </div>
        </>
    }
}

export default AbfParentSummary;