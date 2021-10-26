import Head from 'next/head'
import Image from 'next/image'
import styles from '../styles/Home.module.css'
import Layout from "@components/Layout"; // Layout
import App from "./App.tsx";

function Para(props){

   const variables = Math.random() > 0.5 ? 'red' : 'blue';
   return (
     <div>
       <p className="desc">{props.paragraph}</p>
        <style jsx>
              {`
                .desc {
                  color: ${variables};
                }
              `}
        </style>
     </div>
   )
}

export default function Home() {
  return (
    <Layout>
    {/* <App/> */}
    <div className={styles.container}>
      <Head>
        <title>Create Next App</title>
        <meta name="description" content="Generated by create next app" />
        <link rel="icon" href="/favicon.ico" />
      </Head>

      <main className={styles.main}>

        <Para paragraph="Get started by entering Real Estate information" />
        
        <div className={styles.grid}>
          <a href="/" className={styles.card}>
            <h2>REC &rarr;</h2>
            <p>Information about Real Estate Company!</p>
          </a>
          <a href="/" className={styles.card}>
            <h2>Opportunity &rarr;</h2>
            <p>Information about the avalable opportunity!</p>
          </a>
          <a href="/" className={styles.card}>
            <h2>Review &rarr;</h2>
            <p>Review Assets proposed by REIT!</p>
          </a>
          <a href="/" className={styles.card} >
            <h2>Contract &rarr;</h2>
            <p> Establish contractual agreement with REC </p>
          </a>
        </div>
      </main>

      <footer className={styles.footer}>
        <a
          href=""
          target="_blank"
          rel="noopener noreferrer"
        >
          Powered by{' '}
          <span className={styles.logo}>
            <Image src="/drem.png" alt="DREM marketplace" width={72} height={16} />
          </span>
        </a>
      </footer>
    </div>
    </Layout>
  )
}
