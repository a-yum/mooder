document.addEventListener("DOMContentLoaded", function () {
  var moodCtx = document.getElementById("moodChart").getContext("2d");
  var moodChart = new Chart(moodCtx, {
    type: "pie",
    data: {
      labels: moodData.moods,
      datasets: [
        {
          data: moodData.moodCounts,
          backgroundColor: [
            "#FF0000", // VeryUnhappy - Red
            "#FF7F00", // Unhappy - Orange
            "#FFFF00", // Neutral - Yellow
            "#7FFF00", // Happy - Light Green
            "#00FF00", // VeryHappy - Green
          ],
          hoverBackgroundColor: [
            "#FF0000",
            "#FF7F00",
            "#FFFF00",
            "#7FFF00",
            "#00FF00",
          ],
        },
      ],
    },
    options: {
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        legend: {
          position: "top",
        },
        title: {
          display: false,
          text: "Mood Frequencies for the Previous Month",
        },
        datalabels: {
          color: "#000",
          formatter: (value, ctx) => {
            let sum = ctx.chart.data.datasets[0].data.reduce(
              (a, b) => a + b,
              0
            );
            let percentage = ((value * 100) / sum).toFixed(2) + "%";
            return `${percentage}\n(${value})`;
          },
        },
      },
    },
    plugins: [ChartDataLabels],
  });

  var monthlyCtx = document
    .getElementById("monthlyEntriesChart")
    .getContext("2d");
  var monthlyEntriesChart = new Chart(monthlyCtx, {
    type: "bar",
    data: {
      labels: moodData.months,
      datasets: [
        {
          label: "Number of Entries",
          data: moodData.monthlyEntryCounts,
          backgroundColor: "rgba(75, 192, 192, 0.2)",
          borderColor: "rgba(75, 192, 192, 1)",
          borderWidth: 1,
        },
      ],
    },
    options: {
      responsive: true,
      maintainAspectRatio: false,
      scales: {
        y: {
          beginAtZero: true,
        },
      },
      plugins: {
        legend: {
          display: false,
        },
        title: {
          display: false,
          text: "Number of Entries by Month",
        },
      },
    },
  });
});
