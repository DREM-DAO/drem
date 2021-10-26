import React, { useState } from 'react'
import Layout from '../components/Layout'
import styles from '../styles/Rec.module.css'
import axios from "axios"; // axios requests
//import App from "./App.tsx";

export interface IREC {
  rec: {
      orgName: string
      orgTaxID: string
      orgPhoneNumber: string
      addressLine1: string
      addressLine2?: string
      city: string
      state: string
      country: string
      postalcode: string
      contactFirstName: string
      contactLastName: string
      contactMiddleName?: string
      contactPhoneNumber: string
      contactEmail: string
  }
}

function CreateRec() {

  const [input, setInput] = useState({orgName:"", orgTaxID:"", orgPhoneNumber:"",
          addressLine1: "", addressLine2: "", city: "",  state: "", country: "", postalcode: "",
          contactFirstName:"", contactLastName: "", contactMiddleName:"",
          contactPhoneNumber: "", contactEmail: "",
      }) 

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setInput({ ...input, [e.target.name]: e.target.value })
  }

const handleClick = () => {
    if(!input.orgName || !input.orgTaxID ) return

    setInput({...input, orgName: input.orgName });

    console.log("Input: " ,  {...input} )

//     await axios.post("/api/tweet", { id: '0'})
//         .then( (response) => {
//             console.log("Response:", response);
//             const _data = response.data.data;
//             _data.map( (row) => { 
//               row.text == address ? setTwitterMention(true) : '';
//             })
//     });

}

  return (
    
    <Layout>
    <div className={styles.AddRec}>
          <h1>Real Estate Company Information </h1>
           <hr/>
          <div> 
            Orgnization Name:&nbsp;
            <input type="text" onChange={handleChange} className={styles.RecInput}
                  name="orgName" value={input.orgName} placeholder="orgName"  /> &nbsp;
            Tax ID:&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput}
                  name="orgTaxID" value={input.orgTaxID}  placeholder="orgTaxID"  /> &nbsp;
            Phone Number:&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput} 
                  name="orgPhoneNumber" value={input.orgPhoneNumber}  placeholder="orgPhoneNumber"  />
          </div>

          <hr/>
          <p><b>Organization Address Information: </b></p>
          <div> 
            AddressLine1:&nbsp;
            <input type="text" onChange={handleChange} className={styles.RecInput}
                  name="addressLine1" value={input.addressLine1} placeholder="addressLine1"  /> &nbsp;
            AddressLine2:&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput}
                  name="addressLine2" value={input.addressLine2}  placeholder="addressLine2"  /> &nbsp;
            City:&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput} 
                  name="city" value={input.city}  placeholder="city"  />
          </div>
          <div> 
            State:&nbsp;
            <input type="text" onChange={handleChange} className={styles.RecInput}
                  name="state" value={input.state} placeholder="state"  /> &nbsp;
            Country:&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput}
                  name="country" value={input.country}  placeholder="country"  /> &nbsp;
            Postalcode:&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput} 
                  name="postalcode" value={input.postalcode}  placeholder="postalcode"  />
          </div>

          <hr/>
          <p><b>Oragnization Contact Information: </b></p>
         
          <div> 
            First Name:&nbsp;
            <input type="text" onChange={handleChange} className={styles.RecInput}
                  name="contactFirstName" value={input.contactFirstName} placeholder="contactFirstName"  /> &nbsp;
            Last Name:&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput}
                  name="contactLastName" value={input.contactLastName}  placeholder="contactLastName"  /> &nbsp;
            Middle Name&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput} 
                  name="contactMiddleName" value={input.contactMiddleName}  placeholder="contactMiddleName"  />
          </div>
          
          <div> 
            <label htmlFor="Contact Phone Number">Phone Number: </label>  &nbsp;
            <input type="text" onChange={handleChange} className={styles.RecInput}
                  name="contactPhoneNumber" value={input.contactPhoneNumber} placeholder="contactPhoneNumber"  /> &nbsp;
            Email:&nbsp;
            <input type="text" onChange={handleChange}  className={styles.RecInput}
                  name="contactEmail" value={input.contactEmail}  placeholder="contactEmail"  /> &nbsp;
          </div>

          <div className="line"></div>
        <br/>
        
        <button onClick={handleClick} className={styles.RecBtn} > Save </button>
     
    </div>
    </Layout>
  );
}

export default CreateRec;
