import React from 'react';

/**
 * Shows a parent, its comments, child ABFs, and all their analysis images
 */
class ParentSummary extends React.Component {

    // TODO: use HTTP to get ABF details

    getFilenameWithoutExtension(x) {
        return x.replace(/^.*[\\\/]/, '').replace(/\.[^/.]+$/, "");
    }

    render() {

        if (this.props.cell == null)
            return <div>no parent selected</div>

        return <>
            <div className='p-2 border border-dark' style={{ backgroundColor: this.props.cell.color }}>
                <h1>{this.props.cell["parentID"]}</h1>
                <div class='font-monospace mb-2' style={{ opacity: .2 }}>{this.props.cell.abfPaths[0]}</div>
                <input value={this.props.cell["comment"]} className="w-50" readOnly />
                <button className='ms-2'>Submit</button>
            </div>
            <div className='bg-light border'>
                {
                    this.props.cell.abfPaths.map(x =>
                        <div className='my-2 px-1' key={x}>{this.getFilenameWithoutExtension(x)}
                            <button className='ms-1 btn btn-primary btn-sm'>Copy Path</button>
                            <button className='ms-1 btn btn-success btn-sm'>SetPath</button>
                            <button className='ms-1 btn btn-danger btn-sm'>Ignore</button>
                            <span className='ms-1 text-muted'>Loading details...</span>
                        </div>
                    )
                }
            </div>
            <div>
                {
                    this.props.cell.analysisImages.map(x =>
                        <a href={x} key={x}>
                            <img className='m-3 shadow border border-dark' style={{ maxHeight: '200px' }} src={x} />
                        </a>
                    )
                }
            </div>
        </>;
    }
}

export default ParentSummary;