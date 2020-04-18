// sidebar.js

import React from 'react';
import './sidebar.css';
import { BrowserRouter as Router, Route, Link, withRouter } from "react-router-dom";
import { HomeIcon, ListRichIcon, BoltIcon, BugIcon } from 'react-open-iconic-svg';


class SideNav extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            activePath: props.location.pathname,
            items: [
                {
                  path: '/', /* path is used as id to check which NavItem is active basically */
                  name: 'Home',
                  key: 1 /* Key is required, else console throws error. Does this please you Mr. Browser?! */
                },
                {
                  path: '/types',
                  name: 'Types',
                  key: 2
                },
                {
                  path: '/coverage',
                  name: 'Coverage',
                  key: 3
                },
                {
                  path: '/pokemon',
                  name: 'Pokemon',
                  key: 4
                },
              ]
        }
    }

    onItemClick = (path) => {
        this.setState({ activePath: path });
    }

    render() {
        const { items, activePath } = this.state;
        return(
            <div className="sidebar">
            <div className="top-row">
              <a><div className="navbar-brand">PokeTyper</div></a>
            </div>
                {
                    items.map((item) => {
                        return (
                            <NavItem 
                                path={item.path}
                                name={item.name}
                                onItemClick={this.onItemClick}
                                active={item.path === activePath}
                                key={item.key}
                            />
                        );
                    })
                }
            </div>
        );
    }
}

const RouterSideNav = withRouter(SideNav);

class NavItem extends React.Component {
    handleClick = () => {
        const { path, onItemClick } = this.props;
        onItemClick(path);
    }

    render() {
        const { active } = this.props;
        return(
            <div active={active} className="nav-item">
                <Link to={this.props.path} onClick={this.handleClick}>
                  <HomeIcon height="16" width="16" viewBox="0 0 8 8" className="oi"/>
                  {this.props.name}
                </Link>
            </div>
        );
    }
}

export default class Sidebar extends React.Component {
    render() {
        return (
            <RouterSideNav></RouterSideNav>
        );
    }
}

//export default props => {
//  return (
//    <div className="sidebar">
//      <div className="top-row">
//        <a><div className="navbar-brand">PokeTyper</div></a>
//      </div>
//      <div className="pagescol">
//        <ul className="navitemslist">
//          <li className="nav-item">
//            <a href="/">
//              <HomeIcon height="16" width="16" viewBox="0 0 8 8" className="oi"/>
//              Home
//            </a>
//          </li>
//          <li className="nav-item">
//            <a href="/">
//              <ListRichIcon  height="16" width="16" viewBox="0 0 8 8" className="oi"/>
//              Types
//            </a>
//          </li>
//          <li className="nav-item">
//            <a href="/">
//              <BoltIcon  height="16" width="16" viewBox="0 0 8 8" className="oi"/>
//              Coverage
//            </a>
//          </li>
//          <li className="nav-item">
//            <a href="/">
//              <BugIcon  height="16" width="16" viewBox="0 0 8 8" className="oi"/>
//              Pokemon
//            </a>
//          </li>
//        </ul>
//      </div>
//    </div>
//  );
//};
