<script>
    AOS.init({once: true, offset: 100 });

    document.getElementById('eventBookingForm').addEventListener('submit', function(e) {
        let isValid = true;

    // Check Name
    const nameInput = this.querySelector('input[name="fullName"]');
    if (!nameInput || !nameInput.value.trim()) {
        document.getElementById('grp-name')?.classList.add('invalid');
    isValid = false;
        } else {
        document.getElementById('grp-name')?.classList.remove('invalid');
        }

    // Check Email (LƯU Ý: Vẫn giữ @@ để tránh lỗi)
    const emailInput = this.querySelector('input[name="email"]');
    if (!emailInput || !emailInput.value.trim() || !emailInput.value.includes('@@')) {
        document.getElementById('grp-email')?.classList.add('invalid');
    isValid = false;
        } else {
        document.getElementById('grp-email')?.classList.remove('invalid');
        }

    // Check Phone
    const phoneInput = this.querySelector('input[name="phone"]');
    if (!phoneInput || !phoneInput.value.trim()) {
        document.getElementById('grp-phone')?.classList.add('invalid');
    isValid = false;
        } else {
        document.getElementById('grp-phone')?.classList.remove('invalid');
        }

    if (!isValid) e.preventDefault();
    });

    const inputs = document.querySelectorAll('.form-control-custom');
    inputs.forEach(item => {
        item.addEventListener('input', event => {
            const grp = item.closest('.form-group') || item.closest('#grp-phone');
            if (grp) grp.classList.remove('invalid');
        })
    });
</script>