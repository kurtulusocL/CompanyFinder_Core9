function confirmSetDeleted(controller, action, id) {
    Swal.fire({
        title: 'Emin Misiniz?',
        text: "Bu içeriği Silmek İstiyor Musunuz?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet, Sil!',
        cancelButtonText: 'İptal'
    }).then((result) => {
        if (result.isConfirmed) {
            let url = `/${controller}/${action}/${id}`;
            url = url.replace(/\/+/g, '/');
            window.location.href = url;
        }
    });
}