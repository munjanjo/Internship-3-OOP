using Dump.UI;
using Dump.Data;
using Dump.Services;
SeedData.Seed();
new ReservationServices().CompletePastReservations();
new MainMenu().Show();
