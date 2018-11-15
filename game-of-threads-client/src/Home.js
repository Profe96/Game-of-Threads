import React, { Component } from 'react';

class Home extends Component {
    constructor(props) {
        super(props);

        this.state = { searchTerm: "" };

        this.handleFilterTextChange = this.handleFilterTextChange.bind(this);
        this.signOut = this.signOut.bind(this);
    }

    handleFilterTextChange(filterText) {
        this.setState({ searchTerm: filterText });
    }

    signOut() {
        window.gapi.auth.setToken(null);
        window.gapi.auth.signOut();
        this.props.history.push(`/`);
    }

    render() {
        return (
            <div>
                <SearchBar searchTerm={this.state.searchTerm} onFilterTextChange={this.handleFilterTextChange} />
                <button onClick={this.signOut}>Sign Out</button>
                <ProductTable />
            </div>
        );
    }
}

class SearchBar extends Component {
    constructor(props) {
        super(props);
        this.handleFilterTextChange = this.handleFilterTextChange.bind(this);
    }

    handleFilterTextChange(e) {
        this.props.onFilterTextChange(e.target.value);
    }

    render() {
        return (
            <input type="text" placeholder="Buscar" onChange={this.handleFilterTextChange} value={this.props.searchTerm} />
        )
    }
}

class ProductTable extends Component {
    render() {
        return null;
    }
}

export default Home;