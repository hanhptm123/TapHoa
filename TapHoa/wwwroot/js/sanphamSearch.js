$(document).ready(function () {
    $("#searchForm").on("submit", function (event) {
        event.preventDefault(); // Ngăn form gửi request mặc định

        const keyword = $("#searchInput").val();

        // Gửi yêu cầu tìm kiếm
        $.get("/Sanphams/Search", { keyword: keyword }, function (data) {
            let resultHtml = "";

            if (data.length > 0) {
                data.forEach(product => {
                    resultHtml += `
                        <div class="product-item">
                            <img src="${product.hinhanh}" alt="${product.tensp}" />
                            <h5>${product.tensp}</h5>
                            <p>${product.gia.toLocaleString()} VND</p>
                        </div>
                    `;
                });
            } else {
                resultHtml = "<p>No products found.</p>";
            }

            $("#productResults").html(resultHtml);
        });
    });
});
