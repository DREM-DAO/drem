import Link from "next/link"; // Dynamic routing

// Header
export default function Header() {
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
          </div>
      </div>
    
    </div>
  )
  
}
