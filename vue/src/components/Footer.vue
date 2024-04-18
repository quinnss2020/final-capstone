<template>
    <footer class="footer-section">
        <div class="footer-top">
            <div class="container">
                <div class="row">
                    <div class="col-lg-3 col-md-6 col-sm-6">
                        <div class="widget company-intro-widget">
                            <a href="/" class="site-logo"><img src="../assets/DeltaLogo-Light.svg"
                                    alt="logo"></a>
                            <p class="description">Fulfilling your storage needs since 2024 at several convenient locations across the great state of Ohio.</p>
                            <br>
                            <ul class="company-footer-contact-list">
                                <span><i class="fas fa-phone"></i>Tel: (614) 123-4567</span>
                                <span><i class="fas fa-clock"></i>Mon - Sat 8:00 - 6:00</span>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="footer-bottom">
            <div class="container">
                <div class="row">
                    <div class="col-md-6 col-sm-6 text-sm-left text-center">
                        <span class="copy-right-text">© 2024 <a href="https://petinfo.org/">Delta Storage Solutions</a> All Rights
                            Reserved.</span>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <ul class="terms-privacy d-flex justify-content-sm-end justify-content-center">
                            <li><a href="#">Terms & Conditions</a></li>
                            <li><a href="#">Privacy Policy</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div><!-- footer-bottom end -->
    </footer>
</template>

<script>
import image from "../assets/DeltaLogoDark.svg"

export default {
    data: function () {
        return {
            image: image
        }
    }
}
</script>

<style scoped>

html, body {
    min-height: 100%;
}

section {
    padding: 60px 0;
    min-height: 100vh;
}

a,
a:hover,
a:focus,
a:active {
    text-decoration: none;
    outline: none;
}


ul {
    margin: 0;
    padding: 0;
    list-style: none;
}

img {
    max-width: 100%;
    height: auto;
}

footer {
    padding-left: 20px;
}

.description {
    font-style: italic;
}

.footer-section {
    background-color: #314668;
    position: relative;
    overflow: hidden;
    z-index: 9;
}

.footer-section:before {
    content: '';
    position: absolute;
    top: -146%;
    left: -18%;
    width: 44%;
    height: 257%;
    transform: rotate(54deg);
    background-color: rgb(37, 67, 100);
    -webkit-transform: rotate(54deg);
    -moz-transform: rotate(54deg);
    -ms-transform: rotate(54deg);
    -o-transform: rotate(54deg);
    z-index: -10;
}

.footer-top {
    padding-top: 30px;
    padding-bottom: 30px;
}

.footer-top p,
.company-footer-contact-list li {
    color: #e6e5e5;
}

.company-footer-contact-list {
    margin-top: 10px;
}

.company-footer-contact-list span {

    color: #ffb4069c;
    font-weight: 700;
}

.fa-clock {
    margin-right: 30px;
}

.company-footer-contact-list li+li {
    margin-top: 5px;
}

.company-footer-contact-list li i {
    margin-right: 10px;
    font-size: 20px;
    display: inline-block;
}

.footer-top .site-logo {
    margin-bottom: 25px;
    display: block;
    max-width: 170px;
}

.widget-title {
    text-transform: capitalize;
}

.footer-top .widget-title {
    color: #F4ECE1;
    margin-bottom: 40px;
}

.footer-top .small-post-title a {
    font-family: var(--para-font);
    color: #F4ECE1;
    font-weight: 400;
}

.footer-bottom {
    display: flex;
    justify-content: center;
    align-items: center;
    padding: 13px 0;
    border-top: 1px solid rgba(255, 255, 255, 0.149);
}

.copy-right-text {
    color: #F4ECE1;
}

.copy-right-text a {
    color: #ffb4069c;
}

.terms-privacy li+li {
    margin-left: 30px;
    align-self: center;
}

.terms-privacy li a {
    color: #F4ECE1;
    position: relative;
}

.terms-privacy li a:after {
    position: absolute;
    content: '-';
    color: #F4ECE1;
    display: inline-block;
    top: 0;
    right: -18px;
}

.terms-privacy li+li a:after {
    display: none;
}</style>