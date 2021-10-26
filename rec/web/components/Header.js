import Link from "next/link"; // Dynamic routing
import { useState } from "react"; // State management
import Button from "./Button";
import { authAlgo } from "@containers/index"; // Global state
import { useRouter } from "next/router"; // Router


// Header
export default function Header() {
  const router = useRouter(); // Router navigation
  const [loading, setLoading] = useState(false); // Loading state
  const { address, authenticateAlgo, killSession } = authAlgo.useContainer(); // Global state
  
  const authenticateWithLoading = async () => {
    setLoading(true); // Toggle loading
    await authenticateAlgo(); // Authenticate
    setLoading(false); // Toggle loading
  }

  const disconnect = async () => {
     killSession();
     console.log('Disconnect');
     router.push("/");
  }

  return (
    <div>
      <style jsx>
              {`
                .header {
                  padding: 15px 30px 0px 30px;
                  max-width: 1100px;
                  display: flex;
                  justify-content: space-between;
                  align-items: center;
                  z-index: 9999;
                }
                .headerLogo {
                  justify-content: flex-start;
                  width: 80px;
                  height: 60px;
                }
                .headerMenu {
                  color: #fff;
                  background-color: #0b5468;
                  padding: 13px 35px;
                  font-weight: 1000;
                  font-size: 16px;
                  margin: 0px 5px;
                }
              `}
      </style>
      <div className="header">
          <Link href="/">
            <a> <img className="headerLogo" src="/drem.png" alt="drem-rec" />  </a>
          </Link>
         {
           address ? (
            <div>
                <Link href={`/CreateRec`}>
                  <a className="headerMenu">Create REC</a>
                </Link>
                <Link href={`/oppt`}>
                  <a className="headerMenu">Opportunity</a>
                </Link>
                <Link href={`/review`}>
                  <a className="headerMenu">Review</a>
                </Link>
                <Link href={`/contract`}>
                  <a className="headerMenu">Contract</a>
                </Link>
                <button className="headerMenu" onClick={disconnect}> 
                        {"Disconnect " + address.substr(0, 3) + "..." + address.slice(address.length - 3)}
                </button>
            </div>
           )  :  (
             <button className="headerMenu" onClick={authenticateWithLoading}> Connect</button>
           )
         }   
      </div>
    </div>
  )
  
}
