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
                <ProductTable products={this.state.productsResponse} email={this.props.match.params.id} />
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
            <div className="Searcher">
                <input className="SearchBar" type="text" placeholder="Buscar" onChange={this.handleFilterTextChange} value={this.props.searchTerm} />
                <button className="SearcherButton" onClick={this.handleClick}><i className="fa fa-search"></i></button>
            </div>
        );
    }
}

class ProductTable extends Component {
    render() {
        var products = [];

        this.props.products.forEach(product => {
            products.push(<Product product={product} key={product.id} email={this.props.email} />);
        });

        return (
            <div className="ProductTable">{products}</div>
        );
    }
}

class Product extends Component {
    constructor(props) {
        super(props);

        this.handleClick = this.handleClick.bind(this);
    }

    handleClick() {
        fetch(`/selected?email=${this.props.email}&id=${this.props.product.id}&link=${this.props.product.link}&description=${this.props.product.description}`);
        window.open(this.props.product.link, '_blank');
    }

    render() {
        let product = this.props.product;
        let description = product.description.split(",");
        description = <ul>
            <li>{description[0]}</li>
            <li>{description[1]}</li>
        </ul>

        return (
            <div className="Product">
                <div className="imageDiv">
                    <img src={product.imageUrl} alt="Product" className="ImageProduct" />
                </div>
                <div className="descDiv">
                    <div className="ProductName">{product.name}</div>
                    <div className="ProductDescription">{description}</div>
                    <div className="ProductPrice">{product.price}</div>
                </div>
                <div className="ProductLink" onClick={this.handleClick}><i class="fa fa-link"></i></div>
            </div>
        );
    }
}

export default Home;