import React from 'react';

class AbfInfoLine extends React.Component {

    state = {
        abfInfo: null,
    };

    componentDidMount() {
        const url = `http://192.168.1.9/abf-browser/api/v3/abf-info/?path=` + this.props.abfPath;
        fetch(url)
            .then(response => response.json())
            .then(json => { this.setState({ abfInfo: json }); });
    }

    render() {
        if (this.state.abfInfo == null)
            return <>Loading...</>

        if (this.state.abfInfo.error != null)
            return <mark>ABF API ERROR</mark>

        const protocol = String(this.state.abfInfo.protocol).replace(".pro", "");
        const sweepCount = this.state.abfInfo.sweepCount;
        const sweepLengthSec = this.state.abfInfo.sweepLengthSec;
        const totalLengthSec = sweepLengthSec * this.state.abfInfo.sweepCount;
        const totalLengthMin = Math.round(totalLengthSec / 6) / 10;

        const infoWithoutComment = `${protocol}, ${sweepCount} sweeps, ${sweepLengthSec} sec each, ${totalLengthMin} min`;

        const comment = this.state.abfInfo.tagStrings
            ? (<span className='fw-bold bg-warning mx-1 p-1'>{this.state.abfInfo.tagStrings}</span>)
            : <></>

        return (
            <>
                <span style={{ opacity: .5 }}>
                    {infoWithoutComment}
                </span>
                {comment}
            </>
        );
    }
}

export default AbfInfoLine;