import './css/home.css'
import './Graphs.jsx'
function Home() {
    return (
        <div className="home">
            <div className="home-nav">
            <ul>
                <li><a href="#products">Products</a></li>
                <li><a href="#warehouses">Warehouses</a></li>
                <li><a href="#transits">Transits</a></li>
                <li className="logout-item"><button className="logout-button">Logout</button></li>
            </ul>
            </div>
            &nbsp;
            <div className="home-container">
                <div id="top"></div>
                <div id="chart-container"></div>
            </div>
        </div>
    );
}

export default Home