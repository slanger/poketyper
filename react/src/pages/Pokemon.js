import React from 'react';
import PropTypes from 'prop-types';
import './content.css';

const types = {
  normal: 'normal',
  fire: 'fire',
  water: 'water',
  electric: 'electric',
  grass: 'grass',
  ice: 'ice',
  fighting: 'fighting',
  poison: 'poison',
  ground: 'ground',
  flying: 'flying',
  psychic: 'psychic',
  bug: 'bug',
  rock: 'rock',
  ghost: 'ghost',
  dragon: 'dragon',
  dark: 'dark',
  steel: 'steel',
  fairy: 'fairy',
};

function capFL(inst) {
  return inst.charAt(0).toUpperCase() + inst.slice(1);
}

class Pokemon extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            squad: [{},{},{},{},{},{}],
            typeInfo: null,
            opponent: null
        };
        this.handleChange = this.handleChange.bind(this);
        this.changeSquad = this.changeSquad.bind(this);
        this.calcDamageBoard = this.calcDamageBoard.bind(this);
    }
  
    makeType(pokemon){
      const inputtypes = this.props.pokedex[pokemon];
      if (inputtypes == null) {
        return null;
      }

      let typeObj = {
        'Name': "",
        'Resist2x': [],
        'Resist4x': [],
        'Normal': [],
        'WeakTo2x': [],
        'WeakTo4x': [],
        'Immune': []
      };

      let newScore = this.calcDamageBoard(inputtypes);
      const typeOptions = Object.keys(types);
      for (const key of typeOptions) {
        if (newScore.hasOwnProperty(key)){
          if (newScore[key] === .5) {
            typeObj['Resist2x'].push(key);
          } else if (newScore[key] === .25) {
            typeObj['Resist4x'].push(key);
          } else if (newScore[key] === 2) {
            typeObj['WeakTo2x'].push(key);
          } else if (newScore[key] === 4) {
            typeObj['WeakTo4x'].push(key);
          } else if (newScore[key] === 0) {
            typeObj['Immune'].push(key);
          } else {
            typeObj['Normal'].push(key);
          }
        }
      }
      let newArr = inputtypes.map(i => capFL(i));
      typeObj.Name = newArr.join("/");
      return typeObj;
    }
  
    calcDamageBoard(inputtypes) {
      let newScore = { ...this.props.damageBoard };
      inputtypes.forEach(type => {
        let matchup = this.props.typeChart[type];
        matchup['superEffective'].forEach(element => {newScore[element]*=2});
        matchup['notVeryEffective'].forEach(element => {newScore[element]*=.5});
        matchup['noEffect'].forEach(element => {newScore[element]*=0});
      });
      return newScore;
    }
  
    scoreMon(inputMon) {
      let myMonScore = 0;
      if(this.state.typeInfo){
        const montypes = this.props.pokedex[inputMon];
        const opptypes = this.props.pokedex[this.state.opponent];
        const atkBoard = this.calcDamageBoard(opptypes);
        let atkScore = atkBoard[montypes[0]];
        if (montypes.length > 1){ atkScore+=atkBoard[montypes[1]]}
        
        const defBoard = this.calcDamageBoard(montypes);
        let defScore = defBoard[opptypes[0]];
        if (opptypes.length > 1){ defScore+=defBoard[opptypes[1]]}
        
        myMonScore = atkScore-defScore;
        // damage score of opposing mon against this mon
      }
      return myMonScore;
    }
  
    handleChange(e) {
      if (this.props.pokedex.hasOwnProperty(e.target.value)){
        let typeVal = this.makeType(e.target.value);
        this.setState({ typeInfo: typeVal, opponent: e.target.value});
        //Re-score squad
        //let curSquad = [...this.state.squad];
        //curSquad.forEach((o, i, a) => a[i].Score = this.scoreMon(a[i].Name));
        //this.setState({ squad: curSquad });
      } else {
        this.setState({ typeInfo: null, opponent: null});
      }
    }
  
    changeSquad(index, e) {
      if (this.props.pokedex.hasOwnProperty(e.target.value)){
        let curSquad = [...this.state.squad];
        curSquad[index].Name = e.target.value;
        curSquad[index].Score = this.scoreMon(e.target.value);
        this.setState({ squad: curSquad });
      }
    }

    render() {
        const squad = this.state.squad;
        const dlist = Object.keys(this.props.pokedex).sort().map(function(item){
                return <option key={item} value={item} />;
              });
        let resist2x = "";
        let resist4x = "";
        let Immune = "";
        let WeakTo2x = "";
        let WeakTo4x = "";
        let NormalDamage = "";
        let rankedSquad = squad.slice().sort(function (a, b) {return b.Score-a.Score;});
        const rankedSquadList = rankedSquad.map(function(item){
                return <p>{item.Name} Score:{item.Score}</p>;
              });
        if (this.state.typeInfo) {
          resist2x = this.state.typeInfo.Resist2x.map(function(item){
                  return <li>{item}</li>
                });
          resist4x = this.state.typeInfo.Resist4x.map(function(item){
                  return <li>{item}</li>;
                });
          Immune = this.state.typeInfo.Immune.map(function(item){
                  return <li>{item}</li>;
                });
          WeakTo2x = this.state.typeInfo.WeakTo2x.map(function(item){
                  return <li>{item}</li>;
                });
          WeakTo4x = this.state.typeInfo.WeakTo4x.map(function(item){
                  return <li>{item}</li>;
                });
          NormalDamage = this.state.typeInfo.Normal.map(function(item){
                  return <li>{item}</li>;
                });
        }
        return(
          <div className="contentbox">
            <h3>Pokemon</h3>
            <p>
                Learn about the resistances and weaknesses of any Pokemon. Just enter the Pokemon below. Additionally, select your own team and moves below to see which are best suited to counter that pokemon.
                Currently, only the Pokemon from the Let's Go Pikachu and Let's Go Eevee games (the original
                151 plus some Alolan forms) are supported.
            </p>
            <input type="search" list="mons" placeholder="Which Pokemon?" onChange={this.handleChange}/>
            <datalist id="mons">
              {dlist}
            </datalist>
            {this.state.typeInfo && 
              <div className="typeInfo">
                  <h4><b>{this.state.typeInfo.Name} Type</b></h4>
                  <h5><b>Resistances</b></h5>
                  <div className="row">
                      <div className="col">
                          <h6>Resist 2x</h6>
                          <ul className="list">
                              {resist2x}
                          </ul>
                      </div>
                      <div className="col">
                          <h6>Resist 4x</h6>
                          <ul className="list">
                              {resist4x}
                          </ul>
                      </div>
                      <div className="col">
                          <h6>Immune (0x)</h6>
                          <ul className="list">
                              {Immune}
                          </ul>
                      </div>
                  </div>
                  <h5><b>Weaknesses</b></h5>
                  <div className="row">
                      <div className="col">
                          <h6>Weak To 2x</h6>
                          <ul className="list">
                              {WeakTo2x}
                          </ul>
                      </div>
                      <div className="col">
                          <h6>Weak To 4x</h6>
                          <ul className="list">
                              {WeakTo4x}
                          </ul>
                      </div>
                      <div className="col">
                      </div>
                  </div>
                  <h5><b>Normal Damage (1x)</b></h5>
                  <div className="row">
                    <ul className="list">
                      {NormalDamage}
                    </ul>
                  </div>
              </div>
            }
              <br></br><br></br>
              <h3>Enter Your Squad</h3>
              <p>
                  Enter your 6 pokemon to see how they best counter the pokemon selected above.
              </p>
                  {rankedSquadList}
                  <table>
                    <tr>
                      <th>Pokemon</th>
                      <th>Move 1</th>
                      <th>Move 2</th>
                      <th>Move 3</th>
                      <th>Move 4</th>
                    </tr>
                    <tr>
                      <td><input type="search" list="mons" placeholder="Pokemon 1" onfocus="this.value=''" onChange={(evt) => this.changeSquad(0, evt)}/></td>
                      <td><input type="search" list="" placeholder="Select Move" onfocus="this.value=''"/></td>
                      <td>Move 2</td>
                      <td>Move 3</td>
                      <td>Move 4</td>
                    </tr>
                    <tr>
                      <td><input type="search" list="mons" placeholder="Pokemon 2" onfocus="this.value=''" onChange={(evt) => this.changeSquad(1, evt)}/></td>
                      <td>Move 1</td>
                      <td>Move 2</td>
                      <td>Move 3</td>
                      <td>Move 4</td>
                    </tr>
                    <tr>
                      <td><input type="search" list="mons" placeholder="Pokemon 3" onfocus="this.value=''" onChange={(evt) => this.changeSquad(2, evt)}/></td>
                      <td>Move 1</td>
                      <td>Move 2</td>
                      <td>Move 3</td>
                      <td>Move 4</td>
                    </tr>
                    <tr>
                      <td><input type="search" list="mons" placeholder="Pokemon 4" onfocus="this.value=''" onChange={(evt) => this.changeSquad(3, evt)}/></td>
                      <td>Move 1</td>
                      <td>Move 2</td>
                      <td>Move 3</td>
                      <td>Move 4</td>
                    </tr>
                    <tr>
                      <td><input type="search" list="mons" placeholder="Pokemon 5" onfocus="this.value=''" onChange={(evt) => this.changeSquad(4, evt)}/></td>
                      <td>Move 1</td>
                      <td>Move 2</td>
                      <td>Move 3</td>
                      <td>Move 4</td>
                    </tr>
                    <tr>
                      <td><input type="search" list="mons" placeholder="Pokemon 6" onfocus="this.value=''" onChange={(evt) => this.changeSquad(5, evt)}/></td>
                      <td>Move 1</td>
                      <td>Move 2</td>
                      <td>Move 3</td>
                      <td>Move 4</td>
                    </tr>
                  </table>    
          </div>
      );
    }
}

Pokemon.defaultProps = {
  pokedex: { "Bulbasaur": [types.grass, types.poison],
      "Ivysaur": [types.grass, types.poison],
      "Venusaur": [types.grass, types.poison],
      "Venusaur - Mega": [types.grass, types.poison],
      "Charmander": [types.fire],
      "Charmeleon": [types.fire],
      "Charizard": [types.fire, types.flying],
      "Charizard - Mega X": [types.fire, types.dragon],
      "Charizard - Mega Y": [types.fire, types.flying],
      "Squirtle": [types.water],
      "Wartortle": [types.water],
      "Blastoise": [types.water],
      "Blastoise - Mega": [types.water],
      "Caterpie": [types.bug],
      "Metapod": [types.bug],
      "Butterfree": [types.bug, types.flying],
      "Weedle": [types.bug, types.poison],
      "Kakuna": [types.bug, types.poison],
      "Beedrill": [types.bug, types.poison],
      "Beedrill - Mega": [types.bug, types.poison],
      "Pidgey": [types.normal, types.flying],
      "Pidgeotto": [types.normal, types.flying],
      "Pidgeot": [types.normal, types.flying],
      "Pidgeot - Mega": [types.normal, types.flying],
      "Rattata": [types.normal],
      "Rattata - Alolan": [types.normal, types.dark],
      "Raticate": [types.normal],
      "Raticate - Alolan": [types.normal, types.dark],
      "Spearow": [types.normal, types.flying],
      "Fearow": [types.normal, types.flying],
      "Ekans": [types.poison],
      "Arbok": [types.poison],
      "Pikachu": [types.electric],
      "Raichu": [types.electric],
      "Raichu - Alolan": [types.electric, types.psychic],
      "Sandshrew": [types.ground],
      "Sandshrew - Alolan": [types.ice, types.steel],
      "Sandslash": [types.ground],
      "Sandslash - Alolan": [types.ice, types.steel],
      "Nidoran - Female": [types.poison],
      "Nidorina": [types.poison],
      "Nidoqueen": [types.poison, types.ground],
      "Nidoran - Male": [types.poison],
      "Nidorino": [types.poison],
      "Nidoking": [types.poison, types.ground],
      "Clefairy": [types.fairy],
      "Clefable": [types.fairy],
      "Vulpix": [types.fire],
      "Vulpix - Alolan": [types.ice],
      "Ninetales": [types.fire],
      "Ninetales - Alolan": [types.ice, types.fairy],
      "Jigglypuff": [types.normal, types.fairy],
      "Wigglytuff": [types.normal, types.fairy],
      "Zubat": [types.poison, types.flying],
      "Golbat": [types.poison, types.flying],
      "Oddish": [types.grass, types.poison],
      "Gloom": [types.grass, types.poison],
      "Vileplume": [types.grass, types.poison],
      "Paras": [types.bug, types.grass],
      "Parasect": [types.bug, types.grass],
      "Venonat": [types.bug, types.poison],
      "Venomoth": [types.bug, types.poison],
      "Diglett": [types.ground],
      "Diglett - Alolan": [types.ground, types.steel],
      "Dugtrio": [types.ground],
      "Dugtrio - Alolan": [types.ground, types.steel],
      "Meowth": [types.normal],
      "Meowth - Alolan": [types.dark],
      "Persian": [types.normal],
      "Persian - Alolan": [types.dark],
      "Psyduck": [types.water],
      "Golduck": [types.water],
      "Mankey": [types.fighting],
      "Primeape": [types.fighting],
      "Growlithe": [types.fire],
      "Arcanine": [types.fire],
      "Poliwag": [types.water],
      "Poliwhirl": [types.water],
      "Poliwrath": [types.water, types.fighting],
      "Abra": [types.psychic],
      "Kadabra": [types.psychic],
      "Alakazam": [types.psychic],
      "Alakazam - Mega": [types.psychic],
      "Machop": [types.fighting],
      "Machoke": [types.fighting],
      "Machamp": [types.fighting],
      "Bellsprout": [types.grass, types.poison],
      "Weepinbell": [types.grass, types.poison],
      "Victreebel": [types.grass, types.poison],
      "Tentacool": [types.water, types.poison],
      "Tentacruel": [types.water, types.poison],
      "Geodude": [types.rock, types.ground],
      "Geodude - Alolan": [types.rock, types.electric],
      "Graveler": [types.rock, types.ground],
      "Graveler - Alolan": [types.rock, types.electric],
      "Golem": [types.rock, types.ground],
      "Golem - Alolan": [types.rock, types.electric],
      "Ponyta": [types.fire],
      "Rapidash": [types.fire],
      "Slowpoke": [types.water, types.psychic],
      "Slowbro": [types.water, types.psychic],
      "Slowbro - Mega": [types.water, types.psychic],
      "Magnemite": [types.electric, types.steel],
      "Magneton": [types.electric, types.steel],
      "Farfetch'd": [types.normal, types.flying],
      "Doduo": [types.normal, types.flying],
      "Dodrio": [types.normal, types.flying],
      "Seel": [types.water],
      "Dewgong": [types.water, types.ice],
      "Grimer": [types.poison],
      "Grimer - Alolan": [types.poison, types.dark],
      "Muk": [types.poison],
      "Muk - Alolan": [types.poison, types.dark],
      "Shellder": [types.water],
      "Cloyster": [types.water, types.ice],
      "Gastly": [types.ghost, types.poison],
      "Haunter": [types.ghost, types.poison],
      "Gengar": [types.ghost, types.poison],
      "Gengar - Mega": [types.ghost, types.poison],
      "Onix": [types.rock, types.ground],
      "Drowzee": [types.psychic],
      "Hypno": [types.psychic],
      "Krabby": [types.water],
      "Kingler": [types.water],
      "Voltorb": [types.electric],
      "Electrode": [types.electric],
      "Exeggcute": [types.grass, types.psychic],
      "Exeggutor": [types.grass, types.psychic],
      "Exeggutor - Alolan": [types.grass, types.dragon],
      "Cubone": [types.ground],
      "Marowak": [types.ground],
      "Marowak - Alolan": [types.fire, types.ghost],
      "Hitmonlee": [types.fighting],
      "Hitmonchan": [types.fighting],
      "Lickitung": [types.normal],
      "Koffing": [types.poison],
      "Weezing": [types.poison],
      "Rhyhorn": [types.ground, types.rock],
      "Rhydon": [types.ground, types.rock],
      "Chansey": [types.normal],
      "Tangela": [types.grass],
      "Kangaskhan": [types.normal],
      "Kangaskhan - Mega": [types.normal],
      "Horsea": [types.water],
      "Seadra": [types.water],
      "Goldeen": [types.water],
      "Seaking": [types.water],
      "Staryu": [types.water],
      "Starmie": [types.water, types.psychic],
      "Mr. Mime": [types.psychic, types.fairy],
      "Scyther": [types.bug, types.flying],
      "Jynx": [types.ice, types.psychic],
      "Electabuzz": [types.electric],
      "Magmar": [types.fire],
      "Pinsir": [types.bug],
      "Pinsir - Mega": [types.bug, types.flying],
      "Tauros": [types.normal],
      "Magikarp": [types.water],
      "Gyarados": [types.water, types.flying],
      "Gyarados - Mega": [types.water, types.dark],
      "Lapras": [types.water, types.ice],
      "Ditto": [types.normal],
      "Eevee": [types.normal],
      "Vaporeon": [types.water],
      "Jolteon": [types.electric],
      "Flareon": [types.fire],
      "Porygon": [types.normal],
      "Omanyte": [types.rock, types.water],
      "Omastar": [types.rock, types.water],
      "Kabuto": [types.rock, types.water],
      "Kabutops": [types.rock, types.water],
      "Aerodactyl": [types.rock, types.flying],
      "Aerodactyl - Mega": [types.rock, types.flying],
      "Snorlax": [types.normal],
      "Articuno": [types.ice, types.flying],
      "Zapdos": [types.electric, types.flying],
      "Moltres": [types.fire, types.flying],
      "Dratini": [types.dragon],
      "Dragonair": [types.dragon],
      "Dragonite": [types.dragon, types.flying],
      "Mewtwo": [types.psychic],
      "Mewtwo - Mega X": [types.psychic, types.fighting],
      "Mewtwo - Mega Y": [types.psychic],
      "Mew": [types.psychic],
      "Meltan": [types.steel],
      "Melmetal": [types.steel]
    },
  damageBoard: {
          [types.normal]: 1,
          [types.fire]: 1,
          [types.water]: 1,
          [types.electric]: 1,
          [types.grass]: 1,
          [types.ice]: 1,
          [types.fighting]: 1,
          [types.poison]: 1,
          [types.ground]: 1,
          [types.flying]: 1,
          [types.psychic]: 1,
          [types.bug]: 1,
          [types.rock]: 1,
          [types.ghost]: 1,
          [types.dragon]: 1,
          [types.dark]: 1,
          [types.steel]: 1,
          [types.fairy]: 1
      },
  typeChart: {
        [types.normal]: {
          superEffective: [types.fighting], // fighting is super effective to normal
          notVeryEffective: [],
          noEffect: [types.ghost], // ghost has no effect to normal
        },
        [types.fire]: {
          superEffective: [types.water, types.ground, types.rock],
          notVeryEffective: [types.fire, types.grass, types.ice, types.bug, types.steel, types.fairy],
          noEffect: [],
        },
        [types.water]: {
          superEffective: [types.electric, types.grass],
          notVeryEffective: [types.fire, types.water, types.ice, types.steel],
          noEffect: [],
        },
        [types.electric]: {
          superEffective: [types.ground],
          notVeryEffective: [types.electric, types.flying, types.steel],
          noEffect: [],
        },
        [types.grass]: {
          superEffective: [types.fire, types.ice, types.poison, types.flying, types.bug],
          notVeryEffective: [types.water, types.electric, types.grass, types.ground],
          noEffect: [],
        },
        [types.ice]: {
          superEffective: [types.fire, types.fighting, types.rock, types.steel],
          notVeryEffective: [types.ice],
          noEffect: [],
        },
        [types.fighting]: {
          superEffective: [types.flying, types.psychic, types.fairy],
          notVeryEffective: [types.bug, types.rock, types.dark],
          noEffect: [],
        },
        [types.poison]: {
          superEffective: [types.ground, types.psychic],
          notVeryEffective: [types.grass, types.fighting, types.poison, types.bug, types.fairy],
          noEffect: [],
        },
        [types.ground]: {
          superEffective: [types.water, types.grass, types.ice],
          notVeryEffective: [types.poison, types.rock],
          noEffect: [types.electric],
        },
        [types.flying]: {
          superEffective: [types.electric, types.ice, types.rock],
          notVeryEffective: [types.grass, types.fighting, types.bug],
          noEffect: [types.ground],
        },
        [types.psychic]: {
          superEffective: [types.bug, types.ghost, types.dark],
          notVeryEffective: [types.fighting, types.psychic],
          noEffect: [],
        },
        [types.bug]: {
          superEffective: [types.fire, types.flying, types.rock],
          notVeryEffective: [types.grass, types.fighting, types.ground],
          noEffect: [],
        },
        [types.rock]: {
          superEffective: [types.water, types.grass, types.fighting, types.ground, types.steel],
          notVeryEffective: [types.normal, types.fire, types.poison, types.flying],
          noEffect: [],
        },
        [types.ghost]: {
          superEffective: [types.ghost, types.dark],
          notVeryEffective: [types.poison, types.bug],
          noEffect: [types.normal, types.fighting],
        },
        [types.dragon]: {
          superEffective: [types.ice, types.dragon, types.fairy],
          notVeryEffective: [types.fire, types.water, types.electric, types.grass],
          noEffect: [],
        },
        [types.dark]: {
          superEffective: [types.fighting, types.bug, types.fairy],
          notVeryEffective: [types.ghost, types.dark],
          noEffect: [types.psychic],
        },
        [types.steel]: {
          superEffective: [types.fire, types.fighting, types.ground],
          notVeryEffective: [types.normal, types.grass, types.ice, types.flying, types.psychic, types.bug, types.rock, types.dragon, types.steel, types.fairy],
          noEffect: [types.poison],
        },
        [types.fairy]: {
          superEffective: [types.poison, types.steel],
          notVeryEffective: [types.fighting, types.bug, types.dark],
          noEffect: [types.dragon],
        },
      }
};


export default Pokemon;