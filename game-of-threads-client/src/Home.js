import React, { Component } from 'react';
import { GoogleLogout } from 'react-google-login';
import './Home.css';

class Home extends Component {
    constructor(props) {
        super(props);

        this.state = { searchTerm: "", productsResponse: [] };

        this.handleFilterTextChange = this.handleFilterTextChange.bind(this);
        this.handleClickSearch = this.handleClickSearch.bind(this);
        this.signOut = this.signOut.bind(this);
    }

    handleFilterTextChange(filterText) {
        this.setState({ searchTerm: filterText });
    }

    handleClickSearch() {
        this.callApi().then(res => {
            console.log(res);
            this.setState({ productsResponse: res });
        });
    }

    callApi = async () => {
        const response = await fetch(`/product?searchTerm=${this.state.searchTerm}`);
        const body = await response.json();
        if (response.status !== 200) throw Error(body);
        return body;
    };

    signOut() {
        this.props.history.push(`/`);
    }

    render() {
        return (
            <div>
                <SearchBar searchTerm={this.state.searchTerm} onFilterTextChange={this.handleFilterTextChange} handleClickSearch={this.handleClickSearch} />
                <GoogleLogout buttonText="Logout" onLogoutSuccess={this.signOut} />
                <ProductTable products={this.state.productsResponse} />
            </div>
        );
    }
}

class SearchBar extends Component {
    constructor(props) {
        super(props);
        this.handleFilterTextChange = this.handleFilterTextChange.bind(this);
        this.handleClick = this.handleClick.bind(this);
    }

    handleFilterTextChange(e) {
        this.props.onFilterTextChange(e.target.value);
    }

    handleClick(e) {
        this.props.handleClickSearch();
    }

    render() {
        return (
            <div>
                <input type="text" placeholder="Buscar" onChange={this.handleFilterTextChange} value={this.props.searchTerm} />
                <button onClick={this.handleClick}>Search</button>
            </div>
        );
    }
}

class ProductTable extends Component {
    render() {
        var products = [];

        this.props.products.forEach(product => {
            products.push(<Product product={product} key={product.id} />);
        });

        return (
            <div>{products}</div>
        );
    }
}

class Product extends Component {
    render() {
        let product = this.props.product;
        return (
            <div>
                <img src={product.imageUrl} alt="Product" />
                <div>{product.name}</div>
                <div>{product.description}</div>
                <div>{product.link}</div>
                <div>{product.price}</div>
            </div>
        );
    }
}

export default Home;