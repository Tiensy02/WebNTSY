import * as XLSX from 'xlsx-js-style';

/**
 * @description chuyển mảng thành 1 đối tượng blob để có thể download
 * @param {Array} data mảng chứa bảng cần xuất ra file excel
 * @param {String} title tên tiêu đề của file Excel
 * @returns {Blob} 1 đối tượng chứa nội dung của bảng excel dưới dạng nhị phân
 */
export default function convertDataToExcel(data, title) {
    try
    {
        const wb = XLSX.utils.book_new();
        var ws = XLSX.utils.json_to_sheet(data);
        const newData = [title]
        ws = addRowToSheet(ws, newData)
        ws['!merges'] = [{ s: { r: 0, c: 0 }, e: { r: 0, c: Object.keys(data[0]).length - 1 } }];
        // Tính toán chiều rộng tối thiểu cho từng cột (tên cột và dữ liệu)
        const columnWidths = [];
        const headerRow = data[0]; // Giả sử dữ liệu bảng có ít nhất một dòng
        Object.keys(headerRow).forEach((key, columnIndex) => {
            const columnValue = key ? key.toString() : ''; // Lấy tên cột từ thuộc tính của đối tượng
            const columnWidth = columnValue.length + 5; // Chiều dài tên cột
            columnWidths[columnIndex] = columnWidth;
        });
    
        // Duyệt qua từng dòng dữ liệu để tính chiều rộng cho từng cột dữ liệu
        data.forEach((row) => {
            Object.keys(row).forEach((key, columnIndex) => {
                const columnValue = row[key] ? row[key].toString() : '';
                const columnWidth = columnValue.length + 5;
                if (columnWidth > columnWidths[columnIndex]) {
                    columnWidths[columnIndex] = columnWidth;
                }
            });
        });
    
        // Đặt chiều rộng cho mỗi cột trong worksheet
        ws['!cols'] = columnWidths.map((width) => ({ width }));
    
        const centerBoldLargeStyle = {
            alignment: { horizontal: 'center', vertical: 'center' },
            font: { bold: true, sz: 20 },
        };
    
        ws['A1'].s = centerBoldLargeStyle;
        const range = XLSX.utils.decode_range(ws['!ref']);
        for (let row = range.s.r; row <= range.e.r; row++) {
            for (let col = range.s.c; col <= range.e.c; col++) {
                const cellAddress = XLSX.utils.encode_cell({ r: row, c: col });
                if(row==1) {
                    ws[cellAddress].s = { font: { bold: true}}
                }
                if (ws[cellAddress]) {
                    ws[cellAddress].s = {
                        ...ws[cellAddress].s, border: {
                            top: { style: 'thin', color: { rgb: '000000' } }, 
                            bottom: { style: 'thin', color: { rgb: '000000' } }, 
                            left: { style: 'thin', color: { rgb: '000000' } }, 
                            right: { style: 'thin', color: { rgb: '000000' } } 
                        }
                    }
                }
            }
        }
    
        XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
        // Chuyển đổi workbook thành binary
        const wbout = XLSX.write(wb, { type: 'binary', bookType: 'xlsx' });
    
        // Tạo một ArrayBuffer từ binary
        const arrayBuffer = new ArrayBuffer(wbout.length);
        const view = new Uint8Array(arrayBuffer);
        for (let i = 0; i < wbout.length; i++) {
            view[i] = wbout.charCodeAt(i) & 0xff;
        }
    
        // Tạo Blob từ ArrayBuffer
        const blob = new Blob([arrayBuffer], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
    
        // Trả về Blob để download
        return blob;
    } catch(err) {
        console.error(err);
    }
}
/**
 * 
 * @param {Object} ws 1 đối tượng workSheet
 * @param {Array} newRowData mảng để thêm 1 dòng vào ws
 * @returns ws đã thêm 1 dòng mơi 
 */
function addRowToSheet(ws, newRowData) {
    // Trích xuất dữ liệu hiện có của ws thành một mảng 2 chiều
    const currentData = XLSX.utils.sheet_to_json(ws, { header: 1 });

    // Thêm dòng mới vào đầu mảng dữ liệu
    currentData.unshift(newRowData);

    // Tạo ws mới từ mảng dữ liệu đã được thêm dòng mới
    ws = XLSX.utils.aoa_to_sheet(currentData);

    return ws;
}
