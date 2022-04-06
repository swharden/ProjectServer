import React from 'react';
import AbfInfo from "./AbfInfo";

/**
 * Shows a parent, its comments, child ABFs, and all their analysis images
 */
class ParentSummary extends React.Component {

    // TODO: use HTTP to get ABF details

    getFilenameWithoutExtension(x) {
        return x.replace(/^.*[\\/]/, '').replace(/\.[^/.]+$/, "");
    }

    render() {

        if (this.props.cell == null)
            return <div>no parent selected</div>

        return <>
            <div className='p-2 border border-dark' style={{ backgroundColor: this.props.cell.color }}>
                <h1>{this.props.cell["parentID"]}</h1>
                <div className='font-monospace mb-2' style={{ opacity: .2 }}>{this.props.cell.abfPaths[0]}</div>
                <input value={this.props.cell["comment"]} className="w-50" readOnly />
                <button className='ms-2'>Submit</button>
            </div>
            <div className='bg-light border'>
                {
                    this.props.cell.abfPaths.map(x =>
                        <div className='my-2 px-1' key={x}>{this.getFilenameWithoutExtension(x)}
                            <button className='ms-1 btn btn-primary btn-sm' style={{ fontSize: '.7em' }}>Copy</button>
                            <button className='ms-1 btn btn-success btn-sm' style={{ fontSize: '.7em' }}>SetPath</button>
                            <button className='ms-1 btn btn-danger btn-sm' style={{ fontSize: '.7em' }}>Ignore</button>
                            <div className='ps-1 d-inline'
                                style={{ fontSize: '.8em', fontFamily: 'arial narrow' }}>
                                <AbfInfo abfPath={x} />
                            </div>
                        </div>
                    )
                }
            </div>
            <div>
                {
                    this.props.cell.analysisImages.map(x =>
                        <a href={x} key={x}>
                            <img className='m-3 shadow border border-dark' alt='' style={{ maxHeight: '200px' }} src={x} />
                        </a>
                    )
                }
            </div>
        </>;
    }
}

export default ParentSummary;