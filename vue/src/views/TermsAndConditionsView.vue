<template>
  <div class="terms-container">
    <form v-on:submit.prevent="agree()">
      <div id="box">
        <h1>Delta Storage Solutions Terms and Conditions</h1>
        <h2>Welcome to the Delta Storage Solutions Platform!</h2>
        <p>
          By accessing and using this platform, you agree to be bound by the following terms and conditions ("Terms").
          These Terms govern your use of the platform for bidding on abandoned storage units owned by Delta Storage
          Solutions ("Company"). </p>
        <p>
          1. Eligibility:
          You must be at least 18 years old and have the legal capacity to enter into a binding contract to use the
          platform.
          You must be a registered user of the Delta Storage Solutions storage unit website.</p>
        <p>2. Unit Descriptions and Condition:
          All storage units are sold "AS-IS" and "WHERE-IS." This means the Company makes no warranties or guarantees
          expressed or implied, regarding the contents, condition, or value of the unit's contents. </p>
        <p>3. Bidding Process:
          Unit descriptions, including size and photos (if available), are for informational purposes only and may not be
          entirely accurate. You are encouraged to rely solely on your own inspection of the unit's contents (when
          allowed) before placing a bid.
          Bidding opens and closes according to the specified time listed for each unit auction.
          The Company reserves the right to:
          Extend the bidding period for any reason.
          Reject any bids, including the highest bid.
          Cancel an auction at any time.
          You are responsible for ensuring the accuracy of your bids.</p>
        <p>4. Payment and Removal:
          The winning bidder will be notified by email and must complete payment according to the instructions provided by
          the Company within the specified timeframe.
          Payment methods will be specified for each auction and may include credit card, debit card, or cash (on-site at
          the facility).
          The winning bidder is responsible for removing all contents from the storage unit within the designated
          timeframe after payment is complete.
          The Company reserves the right to dispose of any unclaimed contents after the deadline.
          You are responsible for any associated fees, including overdue storage charges, processing fees, and taxes
          related to the auction win.</p>
        <p>5. Risk of Loss:
          You assume all risk of loss or damage to the contents of the storage unit from the moment you win the auction.
        </p>
        <p>6. Disclaimer of Warranties:
          THE COMPANY DISCLAIMS ALL WARRANTIES, EXPRESS OR IMPLIED, INCLUDING WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
          PARTICULAR PURPOSE, AND TITLE. THE COMPANY DOES NOT WARRANT THAT THE PLATFORM WILL BE UNINTERRUPTED OR
          ERROR-FREE.</p>
        <p>7. Limitation of Liability:
          THE COMPANY SHALL NOT BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, CONSEQUENTIAL, OR PUNITIVE
          DAMAGES ARISING OUT OF OR RELATED TO YOUR USE OF THE PLATFORM OR THE STORAGE UNIT AUCTION, INCLUDING, BUT NOT
          LIMITED TO, DAMAGES FOR PERSONAL INJURY, PROPERTY DAMAGE, LOST PROFITS, OR BUSINESS INTERRUPTION.</p>
        <p>8. Indemnification:
          You agree to indemnify and hold harmless the Company, its officers, directors, employees, agents, and
          affiliates, from and against any and all claims, losses, liabilities, expenses, including attorney's fees,
          arising out of or related to your use of the platform or the storage unit auction.</p>
        <p>9. Governing Law:
          These Terms shall be governed by and construed in accordance with the laws of the State of Ohio, without regard
          to its conflict of laws principles.</p>
        10. Entire Agreement:
        These Terms constitute the entire agreement between you and the Company regarding the subject matter hereof and
        supersede all prior or contemporaneous communications, representations, or agreements, whether oral or written.
        11. Amendment:
        The Company reserves the right to amend these Terms at any time by posting the amended terms on the platform. Your
        continued use of the platform following the posting of amended Terms constitutes your acceptance of the amended
        Terms.
        12. Severability:
        If any provision of these Terms is held to be invalid or unenforceable, such provision shall be struck and the
        remaining provisions shall remain in full force and effect.
        13. Binding Effect:
        These Terms shall be binding upon and inure to the benefit of the parties hereto and their respective successors
        and permitted assigns.
        14. Waiver:
        No waiver of any breach of any provision of these Terms shall constitute a waiver of any other or subsequent
        breach.
        15. Your Agreement:
        By logging in to the platform, you acknowledge that you have read, understood, and agree to be bound by these
        Terms.

        Please note that you will be required to agree to these Terms and Conditions each time you log in to the platform.
        <br>
        <br>
        <div class="checkbox">
          <input type="checkbox" id="userInput" @click="agreed = true" required autofocus />
          <label for="userInput" onclick id="label">
            By clicking on the submit button, I agree to the terms & conditions.
          </label>
          <br>
          <br>
          <v-btn elevation="8" rounded="xl" size="regular" :ripple="true" type="submit">SUBMIT</v-btn>
          <v-btn elevation="8" rounded="xl" size="regular" :ripple="true" @click="logout()">CANCEL</v-btn>
          <br>
          <br>
        </div>
       </div>
    </form>

  </div>
</template>

<script>
import userService from "../services/UserService"; //user
import authService from "../services/AuthService"; //admin

export default {
  components: {},
  data() {
    return {
      user: {
        email: "",
        password: "",
      },
      invalidCredentials: false,
      isConfirmed: false,
      code: '',
      agreed: false,
    };
  },
  methods: {
    login() {
      userService
        .login(this.user)
        .then(response => {
          this.$store.commit("SET_AUTH_TOKEN", response.data.token);
          this.$store.commit("SET_USER", response.data.user);
          if (response.status == 200) {
            this.isConfirmed = true;
          }
          else {
            this.$router.push("/");
          }
        })
        .catch(error => {
          const response = error.response;

          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    },
    confirm() {
      userService
        .confirmEmail(this.user, this.code)
        .then(response => {
          if (response.status == 200) {
            this.$router.push("/");
          }
        })
        .catch(error => {
          const response = error.response;
          this.invalidCredentials = true;
        });
    },
    agree() {
      userService
        .agreeTerms(this.user, this.agreed)
        .then(response => {
          if (response.status == 200) {
            this.$router.push("/");
          }
        })
        .catch(error => {
          const response = error.response;
          this.invalidCredentials = true;
        });
    },

    logout() {
      userService
        .logout()
        .then(response => {
          if (response.status == 200) {
            this.$router.push("/logout");
          }
        })
        .catch(error => {
          const response = error.response;
          this.invalidCredentials = true;
        });
    }

  }
};
</script>

<style scoped>

.terms-container {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}

form {
  color:black;
  flex-direction: column;
  justify-content: center;
  overflow-y: scroll;
  height: 400px;
  width: 800px;
  border: 2px solid #314668;
  border-radius: 1rem;
  padding: 30px;
  padding-top: 10px;
  padding-bottom: 10px;
  background: rgba(244, 236, 225, .2);
  text-align: left;
  margin-top: 30px;
}
::-webkit-scrollbar {
width: 16px;
}

::-webkit-scrollbar-track {
/* box-shadow: inset 0 0 5px grey;  */
border-radius: 2rem;
}

::-webkit-scrollbar-thumb {
background:#314668; 
border-radius: 2rem;
}

::-webkit-scrollbar-thumb:hover {
background:rgb(54, 56, 58); }


.form-input-group {
  margin-bottom: 1rem;
}

button {
  justify-self: center;
  align-items: center;
  margin-right: 20px;
}

label {
  margin-right: 0.5rem;
}

</style>